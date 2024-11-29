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

public class UserMenuState : IState
{
    enum Buttons 
    {
        Operation = 1,
        Support = 2,
        Transfer = 3,
        Settings = 4,
        Referal = 5,
        AdminPanel = 6,
    }
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        int selcted = int.Parse(callbackQuery.Data);
        switch(selcted)
        {
            case (int)Buttons.Operation:
                stateController.SetNewState(new OperationState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.Support:
                stateController.SetNewState(new SupportState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.Transfer:
                stateController.SetNewState(new TransferState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.Settings:
                stateController.SetNewState(new SettingsState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.Referal:
                stateController.SetNewState(new ReferalState(), callbackQuery.Message!.Chat.Id);
                stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case (int)Buttons.AdminPanel:
                stateController.SetNewState(new AdminState(), callbackQuery.Message!.Chat.Id);
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
                .AddButton("Operation", Convert.ToString(Buttons.Operation))
                .AddButton("Support", Convert.ToString(Buttons.Support))
            .AddNewRow()
                .AddButton("Transfer", Convert.ToString(Buttons.Transfer))
                .AddButton("Setings", Convert.ToString(Buttons.Settings))
            .AddNewRow()
                .AddButton("Referal", Convert.ToString(Buttons.Referal));

        if(msg.Chat.Username == "m102983")
        {
            inlineMarkup.AddNewRow().AddButton("AdminPanel", Convert.ToString(Buttons.AdminPanel));
        }
        
        await bot.SendMessage(msg.Chat, $"hello from user menu", ParseMode.Html, replyMarkup: inlineMarkup);
    }
}