using Microsoft.ServiceBus.Messaging;
using ServiceBus.Model;
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
                BrokeredMessage message = new BrokeredMessage(
                    new SampleClass
                    {
                        Property1 = 10,
                        Property2 = "Hello to Service Bus Message Queue at Time " + DateTime.Now,
                        Property3 = true,
                        Created = DateTime.Now,
                        CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name
                    }
                    );

                WorkerRole worker = new WorkerRole();
                worker.SendMessageToQueue(message);

            }
        }
    }
}
