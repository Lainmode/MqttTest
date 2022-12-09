//
//
//
//
using MQTTnet;
using MQTTnet.Server;
using System.Text;

namespace MqttClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Mqtt Factory used to build clients and servers (brokers)
            var mqttFactory = new MqttFactory();

            // client options creation
            var options = mqttFactory.CreateClientOptionsBuilder()
                                     .WithClientId(Guid.NewGuid().ToString())
                                     .WithConnectionUri(new Uri("mqtt://127.0.0.1:1883"))
                                     .Build();

            // create client 
            var mqttClient = mqttFactory.CreateMqttClient();

            // handlers
            mqttClient.ConnectedAsync += async e => { Print("Connected!", ConsoleColor.Cyan); };

            // connect using the previously decalred options
            await mqttClient.ConnectAsync(options);

            await mqttClient.SubscribeAsync(mqttFactory.CreateSubscribeOptionsBuilder()
                                                        .WithTopicFilter(mqttFactory
                                                        .CreateTopicFilterBuilder()
                                                        .WithTopic("home/garden/fountain")
                                                        .Build())
                                                        .Build());

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






