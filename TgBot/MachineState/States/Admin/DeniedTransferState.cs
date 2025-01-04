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

public class DeniedTransferState : IState
{
    enum Buttons 
    {
        Back = 1,
        DeniedTransferState = 2,
        SetMinAmout = 3,
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
                await stateController.SetNewState(new DeniedTransferStateRequestView(0), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.SetMinAmout:

                await stateController.SetNewState(new WithdrawalState(), callbackQuery.Message!.Chat.Id);
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
        Repos.OperationsRepos res =  stateController.Repos.OperationsRepos;
        var models = await res.GetAllModel();
        models = models.FindAll(op => op.OpT != -1);

        var inlineMarkup = new InlineKeyboardMarkup()
                   .AddNewRow()
                       .AddButton("открыть заявки", Convert.ToString(Buttons.DeniedTransferState))
                       .AddButton("Минимальная сумма снятие", Convert.ToString(Buttons.SetMinAmout))
                   .AddNewRow()
                       .AddButton("Назад", Convert.ToString(Buttons.Back));

        await bot.SendMessage(msg.Chat, $"Всего заявок {models.Count}", ParseMode.Html, replyMarkup: inlineMarkup);
        /*        var inlineMarkup = new InlineKeyboardMarkup()
                    .AddNewRow()
                        .AddButton("Заявки на снятие", Convert.ToString(Buttons.DeniedTransferState))
                        .AddButton("Минимальная сумма снятие", Convert.ToString(Buttons.SetMinAmout))
                    .AddNewRow()
                        .AddButton("Назад", Convert.ToString(Buttons.Back));

                await bot.SendMessage(msg.Chat, $"Oper menu", ParseMode.Html, replyMarkup: inlineMarkup);*/
    }
}