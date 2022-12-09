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
            broker.StartedAsync += async e => { Console.WriteLine("Server successfully started!"); };
            broker.ClientConnectedAsync += async e => { Console.WriteLine($"New connection: ID: {e.ClientId} ENDPOINT: {e.Endpoint}"); };
            broker.ClientDisconnectedAsync += async e => { Console.WriteLine($"Client disconnected: ID: {e.ClientId} ENDPOINT: {e.Endpoint}"); };
            broker.ClientSubscribedTopicAsync += async e => { Console.WriteLine(e.TopicFilter.Topic); };
            broker.InterceptingPublishAsync += async e => { Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload)); };

            // start server
            await broker.StartAsync();



            Console.ReadLine();

            await broker.InjectApplicationMessage(new InjectedMqttApplicationMessage(mqttFactory.CreateApplicationMessageBuilder()
                                                                                                .WithTopic("home/garden/fountain")
                                                                                                .WithPayload(Encoding.UTF8.GetBytes("True"))
                                                                                                .Build()));

            Console.ReadLine();

        }


    }
}






