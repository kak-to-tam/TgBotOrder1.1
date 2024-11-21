
using tgBotOrder_v11.DbBot;
using tgBotOrder_v11.Resositories;
using tgBotOrder_v11.Resositories.Model;
using tgBotOrder_v11.Resositories.Abstract;

namespace tgBotOrder_v11.Resositories.Repos;

public class BlacklistRepos : IRepos<BlacklistModel>
{
    TgBotOrderContext tgBotOrderContext;
    public BlacklistRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<BlacklistModel?> CreateModel(int tgID)
    {
        BlacklistModel blacklistModels = new BlacklistModel(tgID);
        return blacklistModels;
    }
    public async Task<List<BlacklistModel>?> GetAllModel()
    {
        List<BlacklistModel> blacklistModel = new List<BlacklistModel>();
        return blacklistModel;
    }
    public async Task<BlacklistModel?> GetModel(int tgID)
    {
        BlacklistModel blacklistModel = new BlacklistModel(tgID);
        return blacklistModel;
    }
    public async Task<BlacklistModel?> Add(BlacklistModel blacklistModel)
    {
        return blacklistModel;
    }
    public async Task<BlacklistModel?> Update(BlacklistModel blacklistModel)
    {
        return blacklistModel;
    }
}