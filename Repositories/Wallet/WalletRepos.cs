
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
    public async Task<WalletModel?> CreateModel(long tgID, string address, float balance)
    {
        WalletModel walletModel = new WalletModel(tgID, address, balance);
        return walletModel;
    }
    public async Task<List<WalletModel>?> GetAllModel()
    {
        List<WalletModel> walletModels = new List<WalletModel>();
        return walletModels;
    }
    public async Task<WalletModel?> GetModel(long tgID)
    {
        WalletModel walletModel = new WalletModel(tgID, "", 0);
        return walletModel;
    }
    public async Task<WalletModel?> Add(WalletModel walletModels)
    {
        return walletModels;
    }
    public async Task<WalletModel?> Update(WalletModel walletModels)
    {
        return walletModels;
    }
}