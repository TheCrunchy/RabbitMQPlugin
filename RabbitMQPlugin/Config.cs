using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQPlugin
{
    public class Config
    {
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Hostname { get; set; } = "localhost";
        public int Port { get; set; } = 5672;

    }
}
