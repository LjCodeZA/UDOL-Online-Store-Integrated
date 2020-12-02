using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OnlineStore.Common
{
    public class RabbitMQConnector
    {
        static IConnection conn = null;
        static IModel channel = null;

        public RabbitMQConnector()
        {
            OpenConnection();
        }

        public void SendOrderToQueue(Models.Order orderDetail)
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = false;

            string message = JsonConvert.SerializeObject(orderDetail);
            byte[] messagebuffer = Encoding.Default.GetBytes(message);

            channel.BasicPublish(Properties.Settings.Default.RabbitMQExchange, Properties.Settings.Default.RabbitMQQueue ,properties, messagebuffer);
        }

        private void OpenConnection()
        {
            string user = Properties.Settings.Default.RabbitMQUser;
            string pass = Properties.Settings.Default.RabbitMQPassword;
            string vhost = Properties.Settings.Default.RabbitMQVhost;

            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = user;
            factory.Password = pass;
            factory.VirtualHost = vhost;

            factory.Uri = new Uri(Properties.Settings.Default.RabbitMQAMQPS);
            conn = factory.CreateConnection();

            channel = conn.CreateModel();
        }

        public void CloseConnection()
        {
            channel.Close();
            conn.Close();
        }
    }
}