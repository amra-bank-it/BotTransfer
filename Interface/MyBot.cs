using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using BotTransfer.Exceptions;
using Microsoft.Extensions.Logging;
using NLog;

namespace BotTransfer.Interface
{
    internal class MyBot
    {
        public ITelegramBotClient bot;

        public MyBot()
        {
            bot = new TelegramBotClient("5231763313:AAHInVO-RpUxs2Qo7uSmCV7NXXndoOmm2kU");
        }

        public void Start()
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            Console.WriteLine("Бот запущен " + bot.GetMeAsync().Result.FirstName);
            _logger.Info("Бот запущен");
            var evt = new AutoResetEvent(false);
            var cts = new CancellationTokenSource();
            var cancellationtoken = cts.Token;
            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = { }
            };
            bot.StartReceiving(BotMain.MainMessage , BotException.respException , receiverOptions , cancellationtoken);
            evt.WaitOne();
        }
    }
}
