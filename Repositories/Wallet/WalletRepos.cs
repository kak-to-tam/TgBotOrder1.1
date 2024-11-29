
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Abstract;

namespace tgBotOrderV11.Repos;

public class WalletRepos : IRepos<WalletModel>
{
    TgBotOrderContext tgBotOrderContext;
    public WalletRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<WalletModel?> CreateModel(long tgID, string address, string privateKey, float balance = 0)
    {
        WalletModel walletModel = new WalletModel(tgID, address, privateKey, balance);
        return walletModel;
    }
    public async Task<List<WalletModel>?> GetAllModel()
    {
        List<WalletModel> walletModels = new List<WalletModel>();
        return walletModels;
    }
    public async Task<WalletModel?> GetModel(long tgID)
    {
        try
        {
            WalletModel walletModel;

            await tgBotOrderContext.Wallets.LoadAsync();
            
            Wallet wallet = tgBotOrderContext.Wallets.Where(needful => needful.UserId == tgID).First();

            walletModel = new WalletModel(wallet.UserId, wallet.Address, wallet.PrivateKey, wallet.Balance);    

            var context = new ValidationContext(walletModel);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(walletModel, context, results, true))
            {
                Console.WriteLine("Не удалось создать объект User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                return walletModel;
        }
        catch(Exception ex)
        {
            return null;
        }

        return null;
    }
    public async Task<WalletModel?> Add(WalletModel walletModels)
    {
        try
        {
            Wallet wallet = new Wallet();
            
            wallet.UserId = walletModels.TgID;
            wallet.Address = walletModels.Address;
            wallet.PrivateKey = walletModels.PrivateKey;
            wallet.Balance = walletModels.Balance;

            tgBotOrderContext.Wallets.Add(wallet);
            
            await tgBotOrderContext.SaveChangesAsync();
            
            return walletModels;
        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }
    public async Task<WalletModel?> Update(WalletModel walletModels)
    {
        try 
        {
            WalletModel walletModelOld = await GetModel(walletModels.TgID);

            if (walletModels.Balance != walletModelOld.Balance)
            {
                Wallet wallet = await tgBotOrderContext.Wallets.Where(needful => needful.UserId == walletModels.TgID).FirstAsync();
                
                wallet.Balance = walletModels.Balance;

                tgBotOrderContext.SaveChanges();
                
                return walletModels;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return null; 
        return walletModels;
    }
}