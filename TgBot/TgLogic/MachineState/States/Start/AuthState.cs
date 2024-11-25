using tgBotOrderV11.TgBot.TgLogic.MachineState;
using tgBotOrder_v11.Resositories.Model;
using tgBotOrder_v11.Resositories.Abstract;
using tgBotOrderV11.TgBot.TgLogic.MachineState;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.TgBot.TgLogic.MachineState.User;

namespace tgBotOrderV11.TgBot.TgLogic.MachineState.Start;

public class AuthState : IState
{
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        stateController.MemoryCache.TryGetValue(msg.Chat.Id, out UserModel? user);
        stateController.MemoryCache.TryGetValue($"um:{msg.Chat.Id}", out UserModel? tempUser);
        if (user != null || tempUser != null)
        {   
            await stateController.SetNewState(new UserMenuState(), msg.Chat.Id);
            stateController.CurrentState.Entry(stateController, msg, bot);
        }
        else 
        {
            UserModel userModel;
            userModel = await stateController.Repos.UserRepos.GetModel(msg.Chat.Id);
            if (userModel != null)
            {
                stateController.MemoryCache.Set(msg.Chat.Id, userModel);
                await stateController.SetNewState(new UserMenuState(), msg.Chat.Id);
                stateController.CurrentState.Entry(stateController, msg, bot);
            }
            else
            {
                stateController.SetNewState(new EmailState(), msg.Chat.Id);
                await bot.SendMessage(msg.Chat, $"hello {msg.Chat.FirstName} u arent reg in system write email" , ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            }
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