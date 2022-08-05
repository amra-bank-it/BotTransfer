using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.Configuration;
using System.Data.SqlClient;
using BotTransfer.Connection;
using Telegram.Bot.Types.ReplyMarkups;
using Sentry;
using SharpRaven;
namespace BotTransfer.WorkKeyboard
{
    internal class BotKeyboard
    {
        public static async Task respKeyboard(ITelegramBotClient botClient, Message message, CallbackQuery callbackQuery)
        {
            if (message == null)
            {
                await botClient.SendTextMessageAsync(callbackQuery.From.Id, callbackQuery.Data.ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Bot_Transfer"].ConnectionString);

                bool answer = false;
                answer = SingletonDB.respBit(Convert.ToInt32(callbackQuery.From.Id));

                con.Open();

                if (answer == false)
                {
                    string queryInsert = $"INSERT INTO Bot_Transfer.dbo.BotTelegram(ChatId) VALUES({callbackQuery.From.Id})";
                    SqlCommand sqlCommand = new SqlCommand(queryInsert, con);
                    sqlCommand.ExecuteNonQuery();
                }
                con.Close();
                return;
            }

            if (message.Text == "/button")
            {
                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                new KeyboardButton[]{"Новый перевод"}
                });
                await botClient.SendTextMessageAsync(message.Chat, text: "...", replyMarkup: keyboard);
                return;
            }                                
        }
    }
}