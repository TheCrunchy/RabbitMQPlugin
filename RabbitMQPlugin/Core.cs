using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Fluent;
using NLog.Targets;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQPlugin.Models;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Managers;
using Torch.Managers.PatchManager;
using Torch.Session;

namespace RabbitMQPlugin
{
    public class Core : TorchPluginBase
    {
        public Logger Log = LogManager.GetLogger("RabbitMQ");
        public static Logger MessageLog = LogManager.GetLogger("MessageLog");

        public static void ApplyLogging()
        {

            var rules = LogManager.Configuration.LoggingRules;

            for (var i = rules.Count - 1; i >= 0; i--)
            {

                var rule = rules[i];

                if (rule.LoggerNamePattern == "MessageLog")
                    rules.RemoveAt(i);
            }



            var logTarget = new FileTarget
            {
                FileName = "Logs/MessageLog-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".txt",
                Layout = "${var:logStamp} ${var:logContent}"
            };

            var logRule = new LoggingRule("MessageLog", LogLevel.Debug, logTarget)
            {
                Final = true
            };

            rules.Insert(0, logRule);

            LogManager.Configuration.Reload();
        }

        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            var sessionManager = Torch.Managers.GetManager<TorchSessionManager>();

            if (sessionManager != null)
            {
                sessionManager.SessionStateChanged += SessionChanged;
            }

            SetupConfig();

        }
        private void SetupConfig()
        {
            var utils = new FileUtils();
            var path = $"{StoragePath}\\RabbitMQConfig.xml";
            if (File.Exists(path))
            {
                config = utils.ReadFromXmlFile<Config>(path);
                utils.WriteToXmlFile<Config>(path, config, false);
            }
            else
            {
                config = new Config();
                utils.WriteToXmlFile<Config>(path, config, false);
            }
            ApplyLogging();
        }

        public static IModel Channel { get; set; }
        private string ExchangeName { get; set; } = "GenericJson";
        private void SessionChanged(ITorchSession session, TorchSessionState newState)
        {
            switch (newState)
            {
                case TorchSessionState.Loaded:
                {
                    var factory = new ConnectionFactory();
                    if (string.IsNullOrWhiteSpace(config.Username))
                    {
                        factory.HostName = config.Hostname;
                    }
                    else
                    {
                        factory.HostName = config.Hostname;
                        factory.UserName = config.Username;
                        factory.Password = config.Password;
                        factory.Port = config.Port;
                    }

                    var connection = factory.CreateConnection();
                    Channel = connection.CreateModel();

                    Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

                    var queueName = Channel.QueueDeclare().QueueName;
                    Channel.QueueBind(queue: queueName,
                        exchange: ExchangeName,
                        routingKey: "");

                    var consumer = new EventingBasicConsumer(Channel);
                    consumer.Received += ReceiveMessage;

                    Channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);
                    break;
                }
                case TorchSessionState.Unloading:
                    Channel.Close();
                    break;
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0)
                ? string.Join(" ", args)
                : throw new ArgumentNullException("Couldnt get message"));
        }

        //get this method from reflection and invoke it 
        public void SendMessage(string MessageType, string JsonMessage)
        {
            var message = new JsonMessage()
            {
                MessageType = MessageType,
                MessageBodyJsonString = JsonMessage
            };

            var json = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);
            Channel.BasicPublish(exchange: ExchangeName,
                routingKey: "",
                basicProperties: null,
                body: body);

            MessageLog.Info($"Sending: {MessageType} {JsonMessage}");
        }

        public void ReceiveMessage(object Model, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                var body = eventArgs.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var Message = JsonConvert.DeserializeObject<JsonMessage>(json);
                var MessageType = Message.MessageType;
                var MessageBody = Message.MessageBodyJsonString;

                MessageLog.Info($"Recieved: {MessageType} {MessageBody}");
                HandleMessage(MessageType, MessageBody);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        //patch this for recieving, you wont need to import any rabbit mq stuff 
        public void HandleMessage(string MessageType, string MessageBody)
        {
            //probably make yourself some enums for messagetype and use a switch statement 
            MessageLog.Info($"Handled: {MessageType} {MessageBody}");
        }

        public static Config config;
    }
}

