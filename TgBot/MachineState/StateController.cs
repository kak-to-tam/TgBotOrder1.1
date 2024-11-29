using tgBotOrderV11.Repos;
using tgBotOrderV11.DbBot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types;
using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using System.Security.Cryptography.X509Certificates;
using tgBotOrderV11.Resos;

namespace tgBotOrderV11.TgBot.TgLogic.MachineState;

public class StateController
{
    public IMemoryCache MemoryCache{ get; private set; }
    public Resositories Repos { get; private set; }
    public IState CurrentState { get; private set; }
    public StateController( IMemoryCache cache, Resositories resositories )
    {
        MemoryCache = cache;
        Repos = resositories;
    }
    public async Task SetNewState(IState newState, long tgID)
    {
        CurrentState = newState;
        MemoryCache.Remove($"st:{tgID}");
        MemoryCache.Set($"st:{tgID}", CurrentState);
    }
}