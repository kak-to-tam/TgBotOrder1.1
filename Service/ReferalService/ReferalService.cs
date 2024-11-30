using System.Drawing;
using Microsoft.Extensions.Caching.Memory;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Resos;
using tgBotOrderV11.TgBot.TgLogic.MachineState;

namespace tgBotOrderV11.TgBot.TgLogic.Service.Referal;


public class ReferalService
{
    private IMemoryCache MemoryCache{ get; init; }
    private Resositories Repos { get; init; }
    public ReferalService(IMemoryCache memoryCache, Resositories repos) 
    {
        MemoryCache = memoryCache;
        Repos = repos;
    }
        
    public async Task<ReferalModel?> AddReferal(long tgID, string refLink)
    {
        byte[] data = Convert.FromBase64String(refLink);
        long refID = long.Parse(System.Text.Encoding.UTF8.GetString(data));

        ReferalModel fatherModel = await Repos.ReferalRepos.GetModel(refID);

        ReferalModel reposModel = await Repos.ReferalRepos.CreateModel(tgID, refID, fatherModel.Father1Id, fatherModel.Father2Id);

        await Repos.ReferalRepos.Add(reposModel);
        return reposModel;
    }

    public async Task AddRefBonus(long tgID, float amout, float percent = (float)0.1)
    {
        ReferalModel fatherModel = await Repos.ReferalRepos.GetModel(tgID);
        if (percent == (float)0.01)
        {
            WalletModel walletModel = await Repos.WalletRepos.GetModel(fatherModel.Father1Id.Value);

            walletModel.Balance += (float)(amout*percent); 

            await Repos.WalletRepos.Update(walletModel);
            return;
        }
        if (fatherModel.Father1Id != 0 && percent != 0)
        {
            WalletModel walletModel = await Repos.WalletRepos.GetModel(fatherModel.Father1Id.Value);

            walletModel.Balance += (float)(amout*percent); 

            await Repos.WalletRepos.Update(walletModel);
            if (fatherModel.Father2Id != 0)
            {
                switch(percent)
                {
                    case (float)0.1:
                        percent = (float)0.5;
                        AddRefBonus(fatherModel.Father1Id.Value, amout, percent);
                        break;
                    case (float)0.05:
                        percent = (float)0.02;
                        AddRefBonus(fatherModel.Father1Id.Value, amout, percent);
                        break;
                    case (float)0.02:
                        percent = (float)0.01;
                        AddRefBonus(fatherModel.Father1Id.Value, amout, percent);
                        break;
                    case (float)0.01:
                        AddRefBonus(fatherModel.Father1Id.Value, amout, percent);
                        break;
                }
            }
        }
        
    }
}