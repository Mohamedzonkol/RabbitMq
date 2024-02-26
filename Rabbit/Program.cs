using RabbitMQ.Client;
using System.Text;

namespace Rabbit
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            GenerateLogs_TopicExchange();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var massege = "log ingo";
                var body =Encoding.UTF8.GetBytes(massege);

                channel.BasicPublish(exchange: "", routingKey: "q1", basicProperties: null, body: body);
            }
        }
        static void GenerateLogs_TopicExchange()
        {
            var factory = new ConnectionFactory() { HostName="localhost"};
            using (var connection = factory.CreateConnection()) 
            using(var channel = connection.CreateModel())
            {
                for (int i =0;i<1000;i++)
                {
                    Thread.Sleep(2000);
                    if (i % 2 == 0)
                    {
                        var massege = $"log info {i}";
                        var body=Encoding.UTF8.GetBytes(massege);
                        channel.BasicPublish(exchange: "amq.topic", routingKey: "log.info",  body: body);

                    }
                    else
                    {
                        var massege = $"log info {i}";
                        var body = Encoding.UTF8.GetBytes(massege);
                        channel.BasicPublish(exchange: "amq.topic", routingKey: "log.error", body: body);

                    }
                }
            }
        }
    }
  
}