using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using ServiceBus.Model;

namespace WorkerRoleWithSBQueue1
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "apolloqueue";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            Client.OnMessage(receivedMessage => ProcessMessage(receivedMessage));
        }

        private void ProcessMessage(BrokeredMessage receivedMessage)
        {
            try
            {
               var complexData = receivedMessage.GetBody<SampleClass>();
                // Process the message
                Console.WriteLine(@"Property1 :: {0}, Property 2 :: {1}, Property 3 :: {2} , Created By :: {3} at Time :: {4}", complexData.Property1, complexData.Property2, complexData.Property3, complexData.CreatedBy, complexData.Created);
                Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                AddDataToStorage addDataToStorage = new AddDataToStorage();
                addDataToStorage.AddDatatoLocalFile(complexData);
            }
            catch (Exception ex)
            {
                // Handle any message processing specific exceptions here
                Trace.WriteLine(ex.Message);
            }

            //Write Queue Process Message to Storage account.
            receivedMessage.Complete();
            CompletedEvent.WaitOne();
            
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

        public QueueClient GetQueueClient()
        {
            if (Client is null)
            {
                this.OnStart();
            }
            return Client;
        }
        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }

        public void SendMessageToQueue(BrokeredMessage message)
        {
            if (Client is null)
            {
                this.OnStart();
            }
            Client.Send(message);
        }
    }
}
