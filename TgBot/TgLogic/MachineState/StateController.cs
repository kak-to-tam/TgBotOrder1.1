using tgBotOrder_v11.Resositories.Repos;
using tgBotOrder_v11.Resositories;
using tgBotOrder_v11.DbBot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types;
using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using System.Security.Cryptography.X509Certificates;

namespace tgBotOrderV11.TgBot.TgLogic.MachineState;

public class StateController
{
    public IMemoryCache MemoryCache{ get; private set; }
    public Resositories Repos{ get; private set; }
    public IState CurrentState;
    public StateController( IMemoryCache cache, Resositories resositories )
    {
        MemoryCache = cache;
        Repos = resositories;
    }
}