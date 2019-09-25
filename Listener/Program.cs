using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    class Program
    {
        private static string topic = "test";
        private static string subscription = "hamzajeljeli";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----   This is a listener   -----");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Gray;
            var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];
            var managementClient = new ManagementClient(serviceBusConnectionString);
            /**
             * Check if the subscription exist or not
             * if it doesn't, it will create it.
             * Else IT WONT WORK.
             */
            if (!(managementClient.SubscriptionExistsAsync(topic, subscription).GetAwaiter().GetResult()))
            {
                managementClient.CreateSubscriptionAsync(new SubscriptionDescription(topic, subscription)).GetAwaiter();
            }
            var subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topic, subscription);
            subscriptionClient.RegisterMessageHandler(async (msg, cancelationToken) =>
            {
                var body = Encoding.UTF8.GetString(msg.Body);
                Console.WriteLine(body);

                await Task.CompletedTask;
            },
            async exception =>
            {
                await Task.CompletedTask;
                Console.WriteLine("Error occured, " + exception.Exception.Message);
                Console.Beep();
            }
            );
            Console.ReadLine();
        }
    }
}
