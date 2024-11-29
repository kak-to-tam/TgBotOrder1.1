using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;
using tgBotOrderV11.DbBot;
using tgBotOrderV11.Repos;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class WithdrawalAmoutState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        if(float.TryParse(msg.Text, out float amout))
        {
            stateController.MemoryCache.TryGetValue("minAmout", out string? value);
            WalletModel walletModel = await stateController.Repos.WalletRepos.GetModel(msg.Chat.Id);
            if (amout > float.Parse(value) && float.Parse(value) < walletModel.Balance)
            {
                
            }

        }
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
        stateController.MemoryCache.TryGetValue("minAmout", out string? value);

        await bot.SendMessage(msg.Chat, $"Введите сумму для вывовода, минимальная ровна {value}", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}