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

            var options = mqttFactory.CreateClientOptionsBuilder()
                                     .WithClientId(Guid.NewGuid().ToString())
                                     .WithConnectionUri(new Uri("mqtt://127.0.0.1:1883"))
                                     .WithWillTopic("home/garden/fountain").Build();

            var mqttClient = mqttFactory.CreateMqttClient();



            Console.ReadLine();

        }


    }
}






