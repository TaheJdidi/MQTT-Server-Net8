using System.Xml.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MQTT_Broker_Backend.Controllers;

using MQTTnet.AspNetCore;
using MQTTnet.Server;
namespace MQTT_Broker_Backend.Services
{
    public class MqttService
    {
        public int ClientCount { get; set; }=0;
        public MqttService()
        {
        }

        public async Task<MqttServer> StartAsync()
        {
            try
            {
                var mqttServerFactory = new MqttServerFactory();
                var mqttServerOptions = mqttServerFactory.CreateServerOptionsBuilder()
                    .WithDefaultEndpoint()
                     .WithDefaultEndpointPort(1888)
                     .Build();
                var server = mqttServerFactory.CreateMqttServer(mqttServerOptions);
                server.ClientConnectedAsync += args =>
                {
                    Console.WriteLine($"Client connected: {args.ClientId}");
                    ClientCount++;
                    return Task.CompletedTask;
                };

                server.ClientDisconnectedAsync += args =>
                {
                    ClientCount--;
                    Console.WriteLine($"Client disconnected: {args.ClientId}");
                    return Task.CompletedTask;
                };
                await server.StartAsync();
                return server;
            }
            catch 
            {
                throw;
            }
            
        }
        
    }
}
