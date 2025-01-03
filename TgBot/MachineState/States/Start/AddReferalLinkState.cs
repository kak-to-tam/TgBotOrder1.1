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
using System.Runtime.InteropServices;


namespace tgBotOrderV11.TgBot.TgLogic.MachineState.Start;

public class AddReferalLinkState : IState
{
    
    public async Task MessHandler(StateController stateController, Message msg, ITelegramBotClient bot)
    {
        const string ErrorMessage = $"Ошибка!";
        if (msg.Text is not null)
        {
            var a = await stateController.ReferalService.AddReferal(msg.Chat.Id, msg.Text);
            if (a is not null)
            {
                await bot.SendMessage(msg.Chat, $"Реферальный код успешно активирован!", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                await bot.SendMessage(msg.Chat, ErrorMessage, ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
            }
        }
        else
        {
            await bot.SendMessage(msg.Chat, ErrorMessage, ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
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
        bot.SendMessage(msg.Chat, $"Ввидите реферальный код", ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }
}