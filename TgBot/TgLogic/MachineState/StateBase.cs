using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types;
using tgBotOrderV11.Repos.Abstract;

namespace tgBotOrderV11.TgBot.TgLogic.MachineState;


public interface IState
{
    public Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot);
    public Task InlineHandler(StateController stateController, CallbackQuery callbackQuery, ITelegramBotClient bot);
    public Task Exit(StateController stateController, Message msg, ITelegramBotClient bot, IState nextState);
    public Task Reset(StateController stateController, Message msg, ITelegramBotClient bot, IState startState);
    public Task Entry(StateController stateController, Message msg, ITelegramBotClient bot);
}

