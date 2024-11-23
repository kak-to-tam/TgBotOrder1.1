using tgBotOrderV11.TgBot.TgLogic.MachineState;
using tgBotOrder_v11.Resositories.Model;
using tgBotOrder_v11.Resositories.Abstract;
using tgBotOrderV11.TgBot.TgLogic.MachineState;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace tgBotOrderV11.TgBot.TgLogic.MachineState.Start;

public class AuthState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
    }
    public async Task InlineHandler(StateController stateController, InlineQuery inlineQuery,  ITelegramBotClient bot)
    {

    }
    public async Task Exit(StateController stateController, Message msg, ITelegramBotClient bot, IState nextState)
    {
        
    }
    public async Task Reset(StateController stateController, Message msg, ITelegramBotClient bot, IState startState)
    {

    }

}