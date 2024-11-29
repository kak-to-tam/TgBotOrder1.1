using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class SetSupportTgState : IState
{

    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        stateController.MemoryCache.Remove("support"); 
        stateController.MemoryCache.Set("support", msg.Text);
        stateController.MemoryCache.TryGetValue("support", out string? supportTg);
        
        await bot.SendMessage(msg.Chat, $"Old support account @{supportTg}", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        
    }
    public async Task Exit(StateController stateController, Message msg, ITelegramBotClient bot, IState nextState)
    {
        
    }
    public async Task Reset(StateController stateController, Message msg, ITelegramBotClient bot, IState startState)
    {

    }
    public async Task Entry(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        stateController.MemoryCache.TryGetValue("support", out string? supportTg);
        
        await bot.SendMessage(msg.Chat, $"Old support account @{supportTg}", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}