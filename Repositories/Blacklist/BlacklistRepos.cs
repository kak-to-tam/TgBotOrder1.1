
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;

namespace tgBotOrderV11.Repos;

public class BlacklistRepos : IRepos<BlacklistModel>
{
    TgBotOrderContext tgBotOrderContext;
    public BlacklistRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<BlacklistModel?> CreateModel(long tgID)
    {
        BlacklistModel blacklistModels = new BlacklistModel(tgID);
        return blacklistModels;
    }
    public async Task<List<BlacklistModel>?> GetAllModel()
    {
        List<BlacklistModel> blacklistModel = new List<BlacklistModel>();
        return blacklistModel;
    }
    public async Task<BlacklistModel?> GetModel(long tgID)
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