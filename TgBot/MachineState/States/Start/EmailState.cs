using tgBotOrderV11.TgBot.TgLogic.MachineState;
using tgBotOrderV11.Repos.Model;
using tgBotOrderV11.Repos.Abstract;
using tgBotOrderV11.TgBot.TgLogic.MachineState;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;
using tgBotOrderV11.Repos;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.Start;

public class EmailState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        UserModel userModel = await stateController.Repos.UserRepos.CreateUserModel(msg.Chat.Id, msg.Text);
        if (userModel == null)
        {
            await bot.SendMessage(msg.Chat, $"non corect email {msg.Chat.FirstName} your input {msg.Text}", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        }
        else
        {
            stateController.MemoryCache.Set($"um:{msg.Chat.Id}", userModel);
            string code = await CodeConfirm.GenerateCode();
            stateController.MemoryCache.Set($"code:{msg.Chat.Id}", code);
            await stateController.SetNewState(new ConfirmEmail(), msg.Chat.Id);
            await EmailSender.SendEmailAsync(userModel.Email, code);
            bot.SendMessage(msg.Chat, $"send code to u email", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
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

    }
}