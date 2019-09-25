using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        private static string topic = "test";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----   This is a publisher   -----");
            Console.WriteLine(" ");
            Console.WriteLine("Hint : Type 'q' to exit.");
            Console.WriteLine(" ");
            var loop = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Write you message : ");
                var message = Console.ReadLine();
                if (message == "q")
                    break;
                var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];
                var topicClient = new TopicClient(serviceBusConnectionString, topic);
                var body = Encoding.UTF8.GetBytes(message);
                var busMessage = new Message(body);
                topicClient.SendAsync(busMessage).GetAwaiter().GetResult();
            } while (loop);
        }
    }
}
