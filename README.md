# ServiceBus and Jobs in Azure with C#
Azure Service Bus with Queue and consumer project.

This project uses Azure Service Bus - Queue to send messages. You need to run ServiceBus.Sender Project(Setup as Startup project) to send messages to Queue on Azure.
Apollo.ServiceBus project is used to process the messages. The queue does not uses Session so FIFO is not gauranteed on the messages.
Project creates a txt file just to display the messages in JSON format.

TODO: Process the Message and insert the complex data {Object} to Storage account and Database.
