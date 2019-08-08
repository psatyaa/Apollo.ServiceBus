using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using StackExchange.Redis;
using System.Configuration;

namespace QueueProcessor
{
    public class Functions
    {
        //[Obsolete]
        //private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    return ConnectionMultiplexer.Connect(ConfigurationSettings.AppSettings["RedisConnectionString"]);
        //});

        //public static ConnectionMultiplexer Connection
        //{
        //    get
        //    {
        //        return lazyConnection.Value;
        //    }
        //}

        //static void AddToRedis(string key, string value)
        //{
        //    IDatabase cache = Connection.GetDatabase();

        //    cache.StringSet(key, value);
        //}


        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([ServiceBusTrigger("apolloqueue")] BrokeredMessage message, TextWriter log)
        {
            log.WriteLine(message);

            log.WriteLine("Message picked up from inboundqueue : " + message.MessageId);
            Stream stream = message.GetBody<Stream>();
            StreamReader reader = new StreamReader(stream);
            string s = reader.ReadToEnd();

            //Log
            log.WriteLine("Message Body : " + s);

            Console.WriteLine("Message Body : " + s);

            //Add to Redis Cache
          //  AddToRedis(message.MessageId, s);

        }
    }
}
