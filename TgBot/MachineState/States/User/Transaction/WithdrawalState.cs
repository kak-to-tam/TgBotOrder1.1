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
        TON = 2,
    }
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        Enum.TryParse(callbackQuery.Data, out Buttons selcted);
        switch (selcted)
        {
            case Buttons.Back:

                stateController.SetNewState(new TransferState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.TON:
                stateController.SetNewState(new WithdrawalAmoutState(), callbackQuery.Message!.Chat.Id);
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
                .AddButton("TON", Convert.ToString(Buttons.TON))
            .AddNewRow()
                .AddButton("Back", Convert.ToString(Buttons.Back));
        
        await bot.SendMessage(msg.Chat, $"Выбирете способо вывода", ParseMode.Html, replyMarkup: inlineMarkup);
    }
}