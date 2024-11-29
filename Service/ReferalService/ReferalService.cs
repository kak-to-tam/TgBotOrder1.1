using System.Drawing;
using Microsoft.Extensions.Caching.Memory;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Resos;
using tgBotOrderV11.TgBot.TgLogic.MachineState;

namespace tgBotOrderV11.TgBot.TgLogic.Systems.Referal;


public class ReferalService
{
    public IMemoryCache MemoryCache{ get; private set; }
    public Resositories Repos { get; private set; }
    public ReferalService(MemoryCache memoryCache, Resositories repos) 
    {
        MemoryCache = memoryCache;
        Repos = repos;
    }
        
    public async Task AddReferal(long tgID, string refLink)
    {
        byte[] data = Convert.FromBase64String(refLink);
        long refID = long.Parse(System.Text.Encoding.UTF8.GetString(data));

        ReferalModel reposModel = await Repos.ReferalRepos.GetModel(refID);

        
    }
}