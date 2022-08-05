using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BotTransfer.WorkMessage;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.Net.Http;
using BotTransfer.Models;
using Newtonsoft.Json;
using NLog;
namespace BotTransfer.Adapters
{
    internal class API_GetReference
    {
        public static string pull(int value)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            string url = "http://404";
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString()
                .Replace(".", "")
                .Replace(":", "")
                .Replace(" ", "");
            WebResponse response;
            string ContentResponse = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://kassa.amra-bank.com/api/v1/payments");

            NetworkCredential myCred = new NetworkCredential("c63054dc-8edc-401e-a648-b372ccc20057", "7793a157-eacb-46a9-8dbb-6085062a5a96");

            request.Headers.Add("Authorization", "Basic YzYzMDU0ZGMtOGVkYy00MDFlLWE2NDgtYjM3MmNjYzIwMDU3Ojc3OTNhMTU3LWVhY2ItNDZhOS04ZGJiLTYwODUwNjJhNWE5Ng==");
            request.Method = "POST";
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Certificate with private key
            request.PreAuthenticate = true;
            var body = @"{" + "\n" +
            @"  ""amount"": {" + "\n" +
            @"    ""currency"": ""RUB""," + "\n" +
            @$"    ""value"": ""{value}.00""" + "\n" +
            @"  }," + "\n" +
            @$"  ""description"": ""Заказ {time}""," + "\n" +
            @"  ""returnUrl"": ""https://merchant.website/return_url""," + "\n" +
            @"  ""metadata"": {" + "\n" +
            @"    ""orderId"": 2022" + "\n" +
            @"  }" + "\n" +
            @"}";

            string postData = body.ToString();//"ЭтоТелоЗапроса
            request.ContentType = "application/json";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postByteArray = encoding.GetBytes(postData);
            request.ContentLength = postByteArray.Length;
            Stream postStream = request.GetRequestStream();
            postStream.Write(postByteArray, 0, postByteArray.Length);
            postStream.Close();
            try
            {
                using (response = request.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                    ContentResponse = reader.ReadToEnd();
                }

               Root root = JsonConvert.DeserializeObject<Root>(ContentResponse);
               url = root.confirmation.confirmationUrl;
               Console.WriteLine($"Получили ссылку на оплату = {root.confirmation.confirmationUrl}");
                _logger.Info($"Получили ссылку на оплату = {root.confirmation.confirmationUrl}");
            }
            catch (Exception ex)
            {
                int r = 0;//
                Console.WriteLine("ERORORORO:" + ex.ToString());
                Console.WriteLine("Текст ссылки --------------> " + url);
                _logger.Error("Произошла ошибка при получении ссылки" + ex.ToString());
            }
            return url;
        }
    }
}