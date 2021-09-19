using System.Threading;
using System;
using System.Collections.Generic;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkNet.Model.Keyboard;

namespace VK_BOT_INSPECTOR
{
    class Program
    {
        static List<string> swearing = new List<string>();
        static readonly VkApi api = new VkApi();

        static Random rnd = new Random();

        static System.Collections.ObjectModel.Collection<Message> messages;

        static long? confId;

        static long? adminId;

        static string token;

        static void Message(string message, long? id)
        {
            try
            {
                api.Messages.Send(new MessagesSendParams
                {
                    RandomId = rnd.Next(0, 1000000000),
                    PeerId = id,
                    Message = message,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("ID Беседы: "); confId = Convert.ToInt64(Console.ReadLine());
            Console.Write("ID Вашей страницы: "); adminId = Convert.ToInt64(Console.ReadLine());
            Console.Write("Токен: "); token = Console.ReadLine();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });
            ;
            int reload = 0;
            while (true)
            {
                MessageGetHistoryObject history = api.Messages.GetHistory(new MessagesGetHistoryParams
                {
                    UserId = confId,
                    Count = 20
                });
                messages = history.Messages.ToCollection();
                int mCount = messages.Count;
                for (int i = 0; i < mCount; i++)
                {
                    if (messages[i].FromId != adminId)
                    {
                        string message = messages[i].Text.ToLower();
                        message = message.Replace(" ", String.Empty).Replace(".", String.Empty).Replace("\\n", String.Empty).Replace("\n", String.Empty).Replace("#", String.Empty).Replace("'", String.Empty);

                        if(message.Contains("Test"))
                        {
                            Message("OK",confId);
                        }
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
