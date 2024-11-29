using tgBotOrderV11.TgBot.TgLogic.MachineState;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;
using tgBotOrderV11.TgBot.TgLogic.MachineState.User;
using tgBotOrderV11.Repos;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.Start;

public class ConfirmEmail : IState
{
    enum Buttons 
    {
        NewEmail = 1,
        NewCode = 2,
        AddReferal = 3,
        NotAdd = 4,
    }
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {

        stateController.MemoryCache.TryGetValue($"code:{msg.Chat.Id}", out string? code);

        if (msg.Text == code)
        {
            stateController.MemoryCache.TryGetValue($"um:{msg.Chat.Id}", out UserModel userModel);
            stateController.MemoryCache.Remove($"um:{msg.Chat.Id}");
            await stateController.Repos.UserRepos.Add(userModel);
            stateController.MemoryCache.Remove($"code:{msg.Chat.Id}");
            var inlineMarkup = new InlineKeyboardMarkup()
            .AddNewRow()
                .AddButton("Добавить реф", Convert.ToString(Buttons.AddReferal))
                .AddButton("Не добавлять", Convert.ToString(Buttons.NotAdd));
            await bot.SendMessage(msg.Chat, $"У вас есть реф код, активируйте его, после завершение регистрации этого сделать нельзя", ParseMode.Html, replyMarkup: inlineMarkup);
        }
        else
        {
            var inlineMarkup = new InlineKeyboardMarkup()
            .AddNewRow()
                .AddButton("new code", Convert.ToString(Buttons.NewCode))
                .AddButton("new email", Convert.ToString(Buttons.NewEmail));
            await bot.SendMessage(msg.Chat, $"non corect code {msg.Chat.FirstName} your input {msg.Text}", ParseMode.Html, replyMarkup: inlineMarkup);
        }
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        Enum.TryParse(callbackQuery.Data, out Buttons selcted);
        switch (selcted)
        {
            case Buttons.NewEmail:
                stateController.MemoryCache.Remove($"um:{callbackQuery.Message!.Chat.Id}");
                stateController.SetNewState(new EmailState(), callbackQuery.Message!.Chat.Id);
                await bot.SendMessage(callbackQuery.Message!.Chat, $"Write new email", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
                break;
            case Buttons.NewCode:
                stateController.MemoryCache.Remove($"code:{callbackQuery.Message!.Chat.Id}");
                string code = await CodeConfirm.GenerateCode();
                stateController.MemoryCache.Set(callbackQuery.Message!.Chat.Id, code);
                await bot.SendMessage(callbackQuery.Message!.Chat, $"We send new code on u email", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
                break;
            case Buttons.AddReferal:
                await stateController.SetNewState(new AddReferalLinkState(), callbackQuery.Message!.Chat.Id);
                await stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
                break;
            case Buttons.NotAdd:
                await stateController.SetNewState(new UserMenuState(), callbackQuery.Message!.Chat.Id);
                await stateController.CurrentState.Entry(stateController, callbackQuery.Message!, bot);
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

    }
}