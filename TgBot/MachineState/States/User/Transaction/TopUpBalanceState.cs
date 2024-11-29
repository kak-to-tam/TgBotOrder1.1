using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;
using tgBotOrderV11.Repos;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class TopUpBalanceState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
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
        WalletModel walletModel;
        
        walletModel = await stateController.Repos.WalletRepos.GetModel(msg.Chat.Id);
        if (walletModel == null)
        {
            
            //stateController.Repos.WalletRepos.CreateModel(msg.Chat.Id );
        }
        await bot.SendMessage(msg.Chat, $"Вот кошелек для пополнения `{walletModel.Address}`", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}