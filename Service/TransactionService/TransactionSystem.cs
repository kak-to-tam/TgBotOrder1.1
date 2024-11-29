


namespace tgBotOrderV11.TgBot.TgLogic.Service;

public enum PaySystem
{
    TRC20,
}

public class TransactionSystem
{
    private TRC20Transaction tRC20Transaction;
    public async Task CreateTransaction(PaySystem paySystem, IOperation operation)
    {
        switch (paySystem)
        {
            case PaySystem.TRC20:
                
                await tRC20Transaction.CreateTransaction(operation);
                break;
        }
    }

    public async Task GetNewTrans(string address)
    {

    }
}