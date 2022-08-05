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
using BotTransfer.WorkMessage;
using BotTransfer.Connection;
using BotTransfer.WorkKeyboard;
using BotTransfer.Adapters;
using NLog;
using Sentry;
namespace BotTransfer
{
    internal class BotMain
    {
        public static async Task MainMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            using (SentrySdk.Init(o =>
            {
                o.Dsn = "https://2e948eda13674393bd12659368a0bb3d@sentry.asar.studio/16";
                o.Debug = true;
                o.TracesSampleRate = 1.0;
            }))

                try
                {
                    Logger _logger = LogManager.GetCurrentClassLogger();
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

                    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                    {
                        var msg = update.Message;
                        string userTxt = msg.Text == null ? "" : msg.Text;
                        MessageType type = userTxt == "/start" ? MessageType.Text : msg.Type;
                        var callbackQuery = update.CallbackQuery;

                        switch (msg.Type)
                        {
                            case MessageType.Text:
                                {
                                    await BotMessage.respMessage(botClient, msg);
                                    break;
                                }
                        }
                        await BotKeyboard.respKeyboard(botClient, msg, callbackQuery);
                        await HandlerKeyboard.handlerKeyboard(botClient, msg);
                        await HandlerMessage.handlerMessage(botClient, msg);
                        SingletonDB.getInstance().getQuery(botClient, msg);
                    }
                    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                    {
                        var msg = update.Message;
                        var callbackQuery = update.CallbackQuery;
                        await BotKeyboard.respKeyboard(botClient, msg, callbackQuery);
                    }
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureException(ex);
                }
                   var transaction = SentrySdk.StartTransaction(
                  "test-transaction-name",
                  "test-transaction-operation"
            );

            var span = transaction.StartChild("test-child-operation");

            span.Finish();
            transaction.Finish();
        }
    }
}