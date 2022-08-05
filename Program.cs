using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using BotTransfer.Interface;
using BotTransfer.Exceptions;
using BotTransfer.WorkMessage;
using BotTransfer.Connection;
using System.Net;
using System.Diagnostics;
using NLog;
using Sentry;
using SharpRaven;
namespace TestBot
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MyBot bot = new MyBot();
                Console.WriteLine(ServicePointManager.SecurityProtocol.ToString());
                bot.Start();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }            
        }
    }
}