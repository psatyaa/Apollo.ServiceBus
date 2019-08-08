using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerRoleWithSBQueue1;

namespace ServiceBus.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= 5; i++)
            {
                BrokeredMessage message = new BrokeredMessage("Number : "+ i);
                WorkerRole worker = new WorkerRole();
                worker.SendMessageToQueue(message);
            }
        }
    }
}
