using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;

namespace UCLMDTCustomizations.Utils

{
	// Implement singleton design pattern for RabbitMQ connection

	public class SingletonRabbit
	{
		private IConnection connection;
		private IModel channel;
		private static SingletonRabbit instance;
		private ConnectionFactory factory;
		private SingletonRabbit()
		{
			factory = new ConnectionFactory();
			IProtocol protocol = Protocols.DefaultProtocol;
			factory.UserName = Properties.Settings.Default.AMQPUsername;
			factory.Password = Properties.Settings.Default.AMQPPassword;
			factory.VirtualHost = Properties.Settings.Default.AMQPVhost;


			factory.Protocol = Protocols.FromEnvironment();
			factory.HostName = Properties.Settings.Default.AMQPHost;
			factory.Port = AmqpTcpEndpoint.UseDefaultPort;
			connection = factory.CreateConnection();
			channel = connection.CreateModel();

		}

		public static SingletonRabbit Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SingletonRabbit();
				}
				return instance;
			}
		}

		public static IModel channelInstance
		{
			get
			{


				IConnection conn = Instance.connection;
				if (!conn.IsOpen)
				{
					Instance.connection = Instance.factory.CreateConnection();
				}
				IModel channel = Instance.channel;
				if (!channel.IsOpen)
				{
					channel = Instance.connection.CreateModel();
					String ExchangeName = Properties.Settings.Default.AMQPExchange;
					channel.ExchangeDeclare(ExchangeName, "fanout");
					channel.QueueDeclare("MDT", true, false, false, null);
					channel.QueueBind("MDT", ExchangeName, "logstash", null);
				}
				return Instance.channel;
			}
		}

	}
}