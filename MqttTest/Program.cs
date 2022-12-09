//
//
//
//
using MQTTnet;
using MQTTnet.Server;
using System.Text;

namespace MqttServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Mqtt Factory used to build clients and servers (brokers)
            var mqttFactory = new MqttFactory();

            // topics
            mqttFactory.CreateTopicFilterBuilder().WithTopic("home/garden/fountain").Build();

            // server options u can specify ip and port 1883 for insecure 8883 for secure
            var options = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

            // server creation
            var broker = mqttFactory.CreateMqttServer(options);




            // Handlers
            broker.StartedAsync += async e => { Print("Server successfully started!", ConsoleColor.Cyan); };
            broker.ClientConnectedAsync += async e => { Print($"New connection: ID: {e.ClientId} ENDPOINT: {e.Endpoint}", ConsoleColor.Green); };
            broker.ClientDisconnectedAsync += async e => { Print($"Client disconnected: ID: {e.ClientId} ENDPOINT: {e.Endpoint}", ConsoleColor.DarkYellow); };
            broker.ClientSubscribedTopicAsync += async e => { Print($"Client subscribed to: {e.TopicFilter.Topic}", ConsoleColor.Blue); };
            broker.InterceptingPublishAsync += async e => { Print($"Data published: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}", ConsoleColor.Magenta); };

            // start server
            await broker.StartAsync();



            Console.ReadLine();

            await broker.InjectApplicationMessage(new InjectedMqttApplicationMessage(mqttFactory.CreateApplicationMessageBuilder()
                                                                                                .WithTopic("home/garden/fountain")
                                                                                                .WithPayload(Encoding.UTF8.GetBytes("True"))
                                                                                                .Build()));

            Console.ReadLine();

        }


        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

        public static void Print(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


    }
}






