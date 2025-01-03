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

public class AdminState : IState
{
    enum Buttons 
    {
        Back = 1,
        DeniedTransferState = 2,
        SetMinAmout = 3,
        SetSupport = 4,
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
                await stateController.SetNewState(new UserMenuState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.DeniedTransferState:
                // i change it here from TopUpBalanceState to DeniedTransferState
                await stateController.SetNewState(new DeniedTransferState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);

                break;
            case Buttons.SetMinAmout:
                await stateController.SetNewState(new SetMinAmoutState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.SetSupport:
                await stateController.SetNewState(new SetSupportTgState(), callbackQuery.Message!.Chat.Id);
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
                .AddButton("Заявки на снятие", Convert.ToString(Buttons.DeniedTransferState))
                .AddButton("Минимальная сумма снятие", Convert.ToString(Buttons.SetMinAmout))
                .AddButton("Саппорт аккаунт", Convert.ToString(Buttons.SetSupport))
            .AddNewRow()
                .AddButton("Назад", Convert.ToString(Buttons.Back));
        
        await bot.SendMessage(msg.Chat, $"Oper menu", ParseMode.Html, replyMarkup: inlineMarkup);
    }
}