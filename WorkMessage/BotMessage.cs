using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Configuration;
using System.Data.SqlClient;
using NLog;
namespace BotTransfer.WorkMessage
{
    public static class BotMessage
    {
        public static async Task respMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text.ToLower() == "/start")
            {
                InlineKeyboardMarkup inKeyboard = new InlineKeyboardMarkup(new[]
                {
               InlineKeyboardButton.WithCallbackData("Да" , $"Введите сумму перевода"),
               InlineKeyboardButton.WithCallbackData("Нет" , "Ваш перевод отменен."),
               });
                await botClient.SendTextMessageAsync(message.Chat, "Здравствуйте\nЭто новый перевод?", replyMarkup: inKeyboard);
                return;
            }
        }
    }
}