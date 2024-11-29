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
using static tgBotOrderV11.Utils.TronAccount;


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
            WalletInfo walletInfo = await TronAccount.Create();

            walletModel = await stateController.Repos.WalletRepos.CreateModel(msg.Chat.Id, walletInfo.pubKey, walletInfo.privateKey);

            await stateController.Repos.WalletRepos.Add(walletModel);
        }
        await bot.SendMessage(msg.Chat, $"Вот адресс кошелька для пополнения `{walletModel.Address}` в течении 5 минут средства поступят на баланс", ParseMode.Markdown, replyMarkup: new ReplyKeyboardRemove());
    }
}