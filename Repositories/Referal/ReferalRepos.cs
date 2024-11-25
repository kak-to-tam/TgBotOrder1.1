
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;

namespace tgBotOrderV11.Repos;

public class ReferalRepos : IRepos<ReferalModel>
{
    TgBotOrderContext tgBotOrderContext;
    public ReferalRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<ReferalModel?> CreateModel(long tgID, long? father1_id = null, long? father2_id = null, long? father3_id = null)
    {
        ReferalModel referalModel = new ReferalModel(tgID, father1_id, father2_id, father3_id);
        return referalModel;
    }
    public async Task<List<ReferalModel>?> GetAllModel()
    {
        List<ReferalModel> referalModels = new List<ReferalModel>();
        return referalModels;
    }
    public async Task<ReferalModel?> GetModel(long tgID)
    {
        ReferalModel referalModel = new ReferalModel(tgID);
        return referalModel;
    }
    public async Task<ReferalModel?> Add(ReferalModel refModel)
    {
        return refModel;
    }
    public async Task<ReferalModel?> Update(ReferalModel refModel)
    {
        return refModel;
    }
}