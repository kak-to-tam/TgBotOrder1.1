using tgBotOrderV11.TgBot.TgLogic.MachineState.Start;
using tgBotOrder_v11.Resositories.Model;
using tgBotOrder_v11.Resositories.Abstract;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tgBotOrderV11.Utils;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.User;

public class UserMenuState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        stateController.MemoryCache.TryGetValue(msg.Chat.Id, out string? code);

        if (msg.Text == code)
        {
            await bot.SendMessage(msg.Chat, $"code is correct", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        }
        else
        {
            var inlineMarkup = new InlineKeyboardMarkup()
            .AddNewRow()
                .AddButton("new code", "nEmail")
                .AddButton("new email", "nCode");
            await bot.SendMessage(msg.Chat, $"non corect code {msg.Chat.FirstName} your input {msg.Text}", ParseMode.Html, replyMarkup: inlineMarkup);
        }
    }
    public async Task InlineHandler(StateController stateController, CallbackQuery callbackQuery,  ITelegramBotClient bot)
    {
        switch(callbackQuery.Data)
        {
            case "nEmail":
                stateController.SetNewState(new EmailState(), callbackQuery.Message!.Chat.Id);
                await bot.SendMessage(callbackQuery.Message!.Chat, $"Write new email", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
                break;
            case "nCode":
                string code = await CodeConfirm.GenerateCode();
                stateController.MemoryCache.Remove($"code:{callbackQuery.Message!.Chat.Id}");
                stateController.MemoryCache.Set(callbackQuery.Message!.Chat.Id, code);
                await bot.SendMessage(callbackQuery.Message!.Chat, $"We send new code on u email", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
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
        await bot.SendMessage(msg.Chat, $"hello from user menu", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}