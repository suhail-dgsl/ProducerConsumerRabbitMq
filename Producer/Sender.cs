
using System.Text;

using RabbitMQ.Client;

public class Sender
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "mashq",
            Port = 5672
        };

        using var connection = factory.CreateConnection();

        using(var channel  = connection.CreateModel())
        {
            channel.QueueDeclare("BasicTest",false,false,false,null);

            var message = "Getting started with dotnet and rabbitMq.";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", "BasicTest", null, body);

            Console.WriteLine("Sent message: {0}", message);
        }

        Console.WriteLine("Press [enter] to exit the Sender App.");
        Console.ReadLine();
    }
}