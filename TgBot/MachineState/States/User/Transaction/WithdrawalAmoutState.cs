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
using TonSdk.Client;
using tgBotOrderV11.TgBot.TgLogic.Service;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class WithdrawalAmoutState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        string[] parts = msg.Text.Split(' ');
        

        if (float.TryParse(parts[0], out float amout))
        {
            stateController.MemoryCache.TryGetValue("minAmout", out string? value);
            WalletModel walletModel = await stateController.Repos.WalletRepos.GetModel(msg.Chat.Id);
            if (amout > float.Parse(value) && float.Parse(value) < walletModel.Balance)
            {
                /*
                 TO DO: send with draw request to admin
                 */

                stateController.Repos.OperationsRepos.CreateModel(msg.From.Id, 0.0, parts[0]);
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

        await bot.SendMessage(msg.Chat, $"Введите сумму и адрес кошелька через пробел например '120 QOS-292340dk3kdssldskf' для вывовода, минимальная ровна {value}", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}