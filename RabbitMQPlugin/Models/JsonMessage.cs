using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQPlugin.Models
{
    public class JsonMessage
    {
        public string MessageType { get; set; }
        public string MessageBodyJsonString { get; set; }
    }
}
