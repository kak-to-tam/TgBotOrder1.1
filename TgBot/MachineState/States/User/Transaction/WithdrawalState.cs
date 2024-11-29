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

public class WithdrawalState : IState
{
    enum Buttons 
    {
        Back = 1,
        TRC20 = 2,
    }
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        int selcted = int.Parse(callbackQuery.Data);
        switch(selcted)
        {
            case (int)Buttons.Back:

                stateController.SetNewState(new TransferState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.TRC20:
                stateController.SetNewState(new TopUpBalanceState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
            break;
        }
    }
    public async Task Exit(StateController stateController, Message msg, ITelegramBotClient bot, IState nextState)
    {
        
    }
    public async Task Reset(StateController stateController, Message msg, ITelegramBotClient bot, IState startState)
    {

    }
    public async Task Entry(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        var inlineMarkup = new InlineKeyboardMarkup()
            .AddNewRow()
                .AddButton("TRC20", Convert.ToString(Buttons.TRC20))
            .AddNewRow()
                .AddButton("Back", Convert.ToString(Buttons.Back));
        
        await bot.SendMessage(msg.Chat, $"Выбирете способо вывода", ParseMode.Html, replyMarkup: inlineMarkup);
    }
}