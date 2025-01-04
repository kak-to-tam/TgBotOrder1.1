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
using System.Diagnostics.Eventing.Reader;
using tgBotOrderV11.TgBot.TgLogic.Service;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class DeniedTransferStateRequestView : IState
{
    enum Buttons 
    {
        Back = 1,
        DeniedTransferState = 2,
        AcceptWithdraw = 3,
        DeclineWithdraw = 4,
        NextWithdraw = 5,
        PrevWithdraw = 6,
        SetMinAmout = 7,
    }

    int id = 0;
    public DeniedTransferStateRequestView(int id)
    {
        this.id = id;
    }

    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
    }

    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        Repos.OperationsRepos res = stateController.Repos.OperationsRepos;
        var models = await res.GetAllModel();
        if (models == null) return;
        models = models.FindAll(op => op.OpT != -1);
        var op = models[id];
        Enum.TryParse(callbackQuery.Data, out Buttons selcted);
        switch (selcted)
        {
            case Buttons.NextWithdraw:
                /*
                 * TO DO: error if id-1 > count
                 */
                await stateController.SetNewState(new DeniedTransferStateRequestView(id+1), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.PrevWithdraw:
                /*
                 * TO DO: error if id-1 < 0
                 */ 
                await stateController.SetNewState(new DeniedTransferStateRequestView(id-1), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.AcceptWithdraw:
                /*
                 * TO DO: accept code
                 */
                switch (op.OpT)
                {
                    case 1:
                        //op.OpT = -1;
                        TransactionSystem.instance.WithDrawTon(op.Amount, op.To, "requested withdraw from MAVRO");
                        break;
                }

                break;
            case Buttons.DeclineWithdraw:

                /*
                 * TO DO: decline code
                 */
                // TO DO: create remove Methonr stateController.Repos.OperationsRepos.Remove(op)
                break;
            case Buttons.Back:
                await stateController.SetNewState(new UserMenuState(), callbackQuery.Message!.Chat.Id);
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
        if (models == null) return;
        models = models.FindAll(op => op.OpT != -1);
        var  op = models[id];

        var inlineMarkup = new InlineKeyboardMarkup()
                   .AddNewRow()
                       .AddButton("Отклонить", Convert.ToString(Buttons.DeclineWithdraw))
                       .AddButton("Подтвердить", Convert.ToString(Buttons.AcceptWithdraw))
                       .AddButton("Следующая", Convert.ToString(Buttons.NextWithdraw))
                       .AddButton("Предыдущая", Convert.ToString(Buttons.PrevWithdraw))
                       .AddButton("Минимальная сумма снятие", Convert.ToString(Buttons.SetMinAmout))
                   .AddNewRow()
                       .AddButton("Назад", Convert.ToString(Buttons.Back));

        await bot.SendMessage(msg.Chat, $"завка N{id}, {op.TgID}, {op.OpT}, {op.To}", ParseMode.Html, replyMarkup: inlineMarkup);
        /*        var inlineMarkup = new InlineKeyboardMarkup()
                    .AddNewRow()
                        .AddButton("Заявки на снятие", Convert.ToString(Buttons.DeniedTransferState))
                        .AddButton("Минимальная сумма снятие", Convert.ToString(Buttons.SetMinAmout))
                    .AddNewRow()
                        .AddButton("Назад", Convert.ToString(Buttons.Back));

                await bot.SendMessage(msg.Chat, $"Oper menu", ParseMode.Html, replyMarkup: inlineMarkup);*/
    }
}