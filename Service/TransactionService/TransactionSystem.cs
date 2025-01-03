using Org.BouncyCastle.Asn1.Mozilla;
using TonSdk.Client;
using TonSdk.Contracts.Jetton;
using TonSdk.Contracts.Wallet;
using TonSdk.Core;
using TonSdk.Core.Block;
using TonSdk.Core.Boc;
using TonSdk.Core.Crypto;

namespace tgBotOrderV11.TgBot.TgLogic.Service;

public enum PaySystem
{
    TRC20,
}

public class TransactionSystem
{

    /*TO DO: fee*/
    static public TransactionSystem instance {
        get { return _instance; }
        set { }
    }

    static private TransactionSystem _instance;
    
    private TonClient _tonClient;
    private WalletV4 _wallet;
    private Mnemonic _mnemonic;
    private Address _jettonMasterContract;
    private Address? _jettonWallet;
    private int _jettonCoef;

    public TransactionSystem(TonClient tonClient, string[] mnemonic, int jettonCoef, string jettonMasterContract)
    {
        _tonClient = tonClient;
        _instance = this;
        
        Mnemonic _mnemonic = new Mnemonic(mnemonic);

        WalletV4Options optionsV4 = new WalletV4Options()
        {
            PublicKey = _mnemonic.Keys.PublicKey
        };

        _wallet = new WalletV4(optionsV4, 2);
        _jettonMasterContract = new Address(jettonMasterContract);
        _jettonCoef = jettonCoef;
    }

    public async void WithDrawTon(decimal amount, string address, string? comment = null)
    {

        Address add = new Address(address, null);
        AddressInformationResult? receier = await _tonClient.GetAddressInformation(add, null);
        Coins _amount = new Coins(amount);

        string memo = comment ?? "EMPTY_MESSAGE";

        Cell body = new CellBuilder().StoreUInt(0, 32).StoreString(memo).Build();
        
        uint? seqno = await _tonClient.Wallet.GetSeqno(_wallet.Address);

        ExternalInMessage tmsg = _wallet.CreateTransferMessage(new[]
        {
            new WalletTransfer
            {
                Message = new InternalMessage(new InternalMessageOptions
                {
                    Info = new IntMsgInfo(new IntMsgInfoOptions
                    {
                        Dest = add,
                        Value = _amount,
                        Bounce = true // make bounceable message
                    }),
                    Body = body
                }),
                Mode = 1 // message mode
            }
        }, seqno ?? 0); // if seqno is null we pass 0, wallet will auto deploy on message send

        // sign transfer message
        tmsg.Sign(_mnemonic.Keys.PrivateKey);

        // get message cell
        Cell cell = tmsg.Cell;

        // send this message via TonClient
        await _tonClient.SendBoc(cell);
    }

    public async void WithDrawCustom(decimal amount, string address, string? comment = null)
    {
        if(_jettonWallet is null)
            _jettonWallet = await _tonClient.Jetton.GetWalletAddress(_jettonMasterContract, _wallet.Address);


        Address receiver = new Address(address);
        Coins _amount = new Coins(amount);

        JettonTransferOptions options = new JettonTransferOptions()
        {
            Amount = _amount,
            Destination = receiver
        };

        Cell jettonTransfer = JettonWallet.CreateTransferRequest(options);

        uint? seqno = await _tonClient.Wallet.GetSeqno(_wallet.Address);

        ExternalInMessage message = _wallet.CreateTransferMessage(new[]
        {
            new WalletTransfer
            {
                Message = new InternalMessage(new InternalMessageOptions
                {
                    Info = new IntMsgInfo(new IntMsgInfoOptions
                    {
                        Dest = _jettonWallet,
                        Value = new Coins(0.1), // amount in TONs to send
                    }),
                    Body = jettonTransfer
                }),
                Mode = 1 // message mode
            }
        }, seqno ?? 0); // if seqno is null we pass 0, wallet will auto deploy on message send


    }
}