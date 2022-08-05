using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotTransfer.WorkKeyboard
{
    internal class HandlerKeyboard
    {
        public static async Task handlerKeyboard(ITelegramBotClient botClient, Message message)
        {
            if (message.Text.ToLower() == "новый перевод")
            {
                InlineKeyboardMarkup inKeyboard = new InlineKeyboardMarkup(new[]
                {
               InlineKeyboardButton.WithCallbackData("Да" , $"Введите сумму перевода"),
               InlineKeyboardButton.WithCallbackData("Нет" , "Ваш перевод отменен"),
               });
                await botClient.SendTextMessageAsync(message.Chat, "Вы уверены?", replyMarkup: inKeyboard);
            }
        }
    }
}
