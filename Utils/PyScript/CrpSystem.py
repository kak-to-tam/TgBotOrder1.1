import sys
from tronpy import Tron
from tronpy.keys import PrivateKey
from tronpy.providers import HTTPProvider

def create_wallet():
    client = Tron()
    
    wallet = client.generate_address()
    
    result =  wallet['base58check_address'] + '|' +  wallet['private_key']
    
    print(result)

def check_balanceUSDT(addr): 
    #private network
    #client = Tron(HTTPProvider("http://127.0.0.1:9090")) 
    
    #nile test network
    client = Tron(network='nile')
    
    #mainnet
    #client = Tron(HTTPProvider(api_key="63899d10-0f97-41e3-8833-feb81ab7fbe4"), network='mainnet')
    
    #USDT
    #contract = client.get_contract('TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t')
    
    #JST
    contract = client.get_contract('TF17BgPaZYbz8oxbjhriubPDsA7ArKoLX3')
    
    precision = contract.functions.decimals()
    
    print(contract.functions.balanceOf(addr) / 10 ** precision)

def check_balanceTRX(addr): 
    #private network
    #client = Tron(HTTPProvider("http://127.0.0.1:9090")) 
    
    #nile test network
    client = Tron(network='nile')
    
    #mainnet
    #client = Tron(HTTPProvider(api_key="63899d10-0f97-41e3-8833-feb81ab7fbe4"), network='mainnet')

    print(client.get_account_balance(addr))   
    
def transactionTRX(addr_from, priv_key, addr_to, amount): 
    
    #private network
    #client = Tron(HTTPProvider("http://127.0.0.1:9090")) 
    
    #nile test network
    client = Tron(network='nile')
    
    #mainnet
    #client = Tron(HTTPProvider(api_key="63899d10-0f97-41e3-8833-feb81ab7fbe4"), network='mainnet')  
    txn = (
        client.trx.transfer(addr_from, addr_to, amount)
        .build()
        .sign(PrivateKey(bytes.fromhex(priv_key)))
    )
    
    txn.broadcast().wait()
    print("SUCCESS")
    
def transactionUSDT(addr_from, priv_key, addr_to, amount): 
    #private network
    #client = Tron(HTTPProvider("http://127.0.0.1:9090")) 
    
    #nile test network
    client = Tron(network='nile')
    
    #mainnet
    #client = Tron(HTTPProvider(api_key="63899d10-0f97-41e3-8833-feb81ab7fbe4"), network='mainnet')
    
    #USDT
    #contract = client.get_contract('TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t')
    
    #JST
    contract = client.get_contract('TF17BgPaZYbz8oxbjhriubPDsA7ArKoLX3')
    
    txn = (
        contract.functions.transfer(addr_to, amount)
        .with_owner(addr_from)
        .fee_limit(5_000_000_000)
        .build()
        .sign(PrivateKey(bytes.fromhex(priv_key)))
        )

    res = txn.broadcast().wait()
    print(res["receipt"]["result"])
    
def swap(addr_from, priv_key): 
    #private network
    #client = Tron(HTTPProvider("http://127.0.0.1:9090")) 
    
    #nile test network
    client = Tron(network='nile')
    
    #mainnet
    #client = Tron(HTTPProvider(api_key="63899d10-0f97-41e3-8833-feb81ab7fbe4"), network='mainnet')
    
    #USDT
    #contract = client.get_contract('TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t')
    
    #JST
    contract = client.get_contract('TB6xBCixqRPUSKiXb45ky1GhChFJ7qrfFj')
    
    for i in contract.functions:
        print(i)
    
    # txn = (
    #     contract.functions.transfer(addr_to, amount)
    #     .with_owner(addr_from)
    #     .fee_limit(5_000_000_000)
    #     .build()
    #     .sign(PrivateKey(bytes.fromhex(priv_key)))
    #     )

    # res = txn.broadcast().wait()
    # print(res["receipt"]["result"])
    
    
if __name__ == "__main__": 
    
    #main TFf5bxPabNEkjKvuphoAEfYoFDK4oFWQGZ 02999e4b2339b6beafeb950a70ccd85c161bc8c332950c17014fd78a42181246
    #sub TUY5vAsWsMThoJhWQMnGdLiCC5V8PxDumj 6327b7051f564bf622e1097fbcdaff5d79d18e9e65ac16ba9a5e49e19e6a5f72
    match int(input()):
    # match int([sys.argv[1]]):
        case 1:
            create_wallet()
            pass
        case 2:
            # transactionTRX([sys.argv[2]], 
            #             [sys.argv[3]], 
            #             [sys.argv[4]],
            #             int([sys.argv[5]]))
            
            transactionTRX("TFf5bxPabNEkjKvuphoAEfYoFDK4oFWQGZ", 
                        "02999e4b2339b6beafeb950a70ccd85c161bc8c332950c17014fd78a42181246", 
                        "TUY5vAsWsMThoJhWQMnGdLiCC5V8PxDumj", 1000000)
            pass
        case 3:
            # transactionTRX([sys.argv[2]], 
            #             [sys.argv[3]], 
            #             [sys.argv[4]],
            #             int([sys.argv[5]]))
            
            transactionUSDT("TFf5bxPabNEkjKvuphoAEfYoFDK4oFWQGZ",
                            "02999e4b2339b6beafeb950a70ccd85c161bc8c332950c17014fd78a42181246",
                            "TUY5vAsWsMThoJhWQMnGdLiCC5V8PxDumj", int(1000000000000000000))
            pass
        case 4:
            #check_balanceTRX([sys.argv[2]])
            check_balanceTRX("TUY5vAsWsMThoJhWQMnGdLiCC5V8PxDumj")
            pass
        case 5:
            #check_balanceUSDT([sys.argv[2]])
            check_balanceUSDT("TUY5vAsWsMThoJhWQMnGdLiCC5V8PxDumj")
            pass
        case 6: 
            swap("TFf5bxPabNEkjKvuphoAEfYoFDK4oFWQGZ", "02999e4b2339b6beafeb950a70ccd85c161bc8c332950c17014fd78a42181246")
    
