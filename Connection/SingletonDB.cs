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
using NLog;
namespace BotTransfer.Connection
{
    internal class SingletonDB
    {
        private static SingletonDB instance;
        private SingletonDB()
        { }

        public static SingletonDB getInstance()
        {
            if (instance == null)
                instance = new SingletonDB();
            return instance;
        }
        public bool getQuery(ITelegramBotClient botClient, Message message)
        {
            bool answer = false;
            if (message.Text.ToLower() == "/start")
            {
                int ch = Convert.ToInt32(message.Chat.Id);
                answer = respBit(ch);
            }
            return answer;
        }
        public static bool respBit(int chatId)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Bot_Transfer"].ConnectionString);
            con.Open();
            bool answer = false;
            string querySelect = $"SELECT ChatId FROM Bot_Transfer.dbo.BotTelegram WHERE ChatId = {chatId}";
            SqlCommand command = new SqlCommand(querySelect, con);
            SqlDataReader reader = command.ExecuteReader();
            string idfromBase = null;
            while (reader.Read())
            {
                idfromBase = reader[0].ToString();
                Console.WriteLine(answer);
            }
            if (idfromBase == null)
            {
                answer = false;
            }
            else
            {
                answer = true;
            }
            reader.Close();
            con.Close();
            return answer;
        }
    }
}
