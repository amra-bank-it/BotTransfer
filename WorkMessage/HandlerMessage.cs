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
using BotTransfer.Adapters;
using NLog;
namespace BotTransfer.WorkMessage
{
    internal class HandlerMessage
    {
        public static async Task handlerMessage(ITelegramBotClient botClient, Message message)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            bool flag = true;
            int value = 0;
            if (message.Text.ToLower() == "да")
            {
                await botClient.SendTextMessageAsync(message.Chat, $"Введите сумму перевода");
            }
            else if (message.Text.ToLower() == "нет")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Ваш перевод отменен.\nДля создания нового перевода воспользуйтесь кнопкой новый перевод");
            }

            try
            {
                string money = message.Text.ToLower().Trim(new Char[] { ' ', '*', '.', ',', 'р' });

                if (money == null)
                {
                    money = "...";
                }
                value = Convert.ToInt32(money);
            }
            catch (Exception e)
            {
                flag = false;
            }
            if (flag == true)
            {
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Получили значение ссылки");
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Bot_Transfer"].ConnectionString);
                con.Open();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Открыли коннекшн");
                _logger.Info("Открыли коннекшн");
                string queryUpdate = $"UPDATE Bot_Transfer.dbo.BotTelegram SET Amount={value} WHERE ChatId={Convert.ToInt32(message.Chat.Id)}";
                SqlCommand cmd = new SqlCommand(queryUpdate, con);
                cmd.ExecuteNonQuery();
                con.Close();
                string url = API_GetReference.pull(value);
                Console.WriteLine("Ввели число");
                if (value > 0)
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Значение больше 0");
                    var plainTextBytes = Encoding.UTF8.GetBytes(url);
                    InlineKeyboardMarkup inKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithUrl("Получить ссылку",url)
                    });
                    await botClient.SendTextMessageAsync(message.Chat, "...", replyMarkup: inKeyboard);
                }
            }
        }
    }
}