
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Org.BouncyCastle.Asn1.Mozilla;

namespace tgBotOrderV11.Repos;

public class OperationsRepos : IRepos<OperationsModel>
{
    TgBotOrderContext tgBotOrderContext;
    public OperationsRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<OperationsModel?> CreateModel(long tgID, float from, float to)
    {
        OperationsModel blacklistModels = new OperationsModel(tgID, from, to);
        return blacklistModels;
    }
    public async Task<List<OperationsModel>?> GetAllModel()
    {
        List<OperationsModel> blacklistModel = new List<OperationsModel>();
        return blacklistModel;
    }
    public async Task<OperationsModel?> GetModel(long tgID)
    {
        OperationsModel blacklistModel = new OperationsModel(tgID);
        return blacklistModel;
    }

    public async Task<OperationsModel?> Add(OperationsModel blacklistModel)
    {
        return blacklistModel;
    }
    public async Task<OperationsModel?> Update(OperationsModel blacklistModel)
    {
        return blacklistModel;
    }
}