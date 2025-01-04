
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tgBotOrderV11.Repos;

public class ReferalRepos : IRepos<ReferalModel>
{
    TgBotOrderContext tgBotOrderContext;
    public ReferalRepos(TgBotOrderContext tgBotOrderContext)
    {
        this.tgBotOrderContext = tgBotOrderContext;
    }
    public async Task<ReferalModel?> CreateModel(long tgID, long? father1_id = 0, long? father2_id = 0, long? father3_id = 0)
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
        try
        {
            ReferalModel referalModel;

            await tgBotOrderContext.Referals.LoadAsync();

            List<Referal> referals = tgBotOrderContext.Referals.Where(needful => needful.UserId == tgID).ToList();
            Referal referal = null;

            if (referals.Count > 0)
            {
                referal = referals.First();

            }
            else return null;

            referalModel = new ReferalModel(referal.UserId, referal.Father1Id, referal.Father2Id, referal.Father3Id);    

            var context = new ValidationContext(referalModel);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(referalModel, context, results, true))
            {
                Console.WriteLine("Не удалось создать объект User");
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                return referalModel ;
        }
        catch(Exception ex)
        {
            return null;
        }

        return null;
        
    }
    public async Task<ReferalModel?> Add(ReferalModel refModel)
    {
        try
        {
            Referal referal = new Referal();
            
            referal.UserId = refModel.TgID;
            referal.Father1Id = refModel.Father1Id;
            referal.Father2Id = refModel.Father2Id;
            referal.Father3Id = refModel.Father3Id;

            tgBotOrderContext.Referals.Add(referal);
            
            await tgBotOrderContext.SaveChangesAsync();
            return refModel;
        }
        catch (Exception ex)
        {
            return null;
        }
        return null;
    }
    public async Task<ReferalModel?> Update(ReferalModel refModel)
    {
        return refModel;
    }
}