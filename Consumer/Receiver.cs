using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Receiver
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "mashq",
            Port = 5672
        };

        using var connection = factory.CreateConnection();

        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare("BasicTest", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.Span;
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(message);
            };

            channel.BasicConsume("BasicTest", true, consumer);
        }

        Console.WriteLine("Press [enter] to exit the Sender application...");
        Console.ReadLine();
    }
}