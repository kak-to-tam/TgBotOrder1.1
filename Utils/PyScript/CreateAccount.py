from tronpy import Tron

client = Tron()

# создаем кошелек и печатаем его данные
def create_wallet(): 
    wallet = client.generate_address()
    
    result =  wallet['base58check_address'] + '|' +  wallet['private_key']
    
    print(result)
    
create_wallet()