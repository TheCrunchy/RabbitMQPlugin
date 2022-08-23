using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQPlugin.Models;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace RabbitMQPlugin
{
    [Category("rabbit")]
    public class Commands : CommandModule
    {
        [Command("sendtext", "send a simple text message")]
        [Permission(MyPromoteLevel.Admin)]
        public void MQTest(string messageText)
        {
            var message = new JsonMessage()
            {
                MessageType = "Text",
                MessageBodyJsonString = messageText
            };

            var json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);
            Core.Channel.BasicPublish(exchange: "GenericJson",
                routingKey: "",
                basicProperties: null,
                body: body);

            Core.MessageLog.Info($"Sending: {message.MessageType} {message.MessageBodyJsonString}");
        }

    }
}

