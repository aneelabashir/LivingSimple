using AccountManagementEventHandler.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace AccountManagementEventHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            IConnection connection = factory.CreateConnection();
            IModel model = connection.CreateModel();
            model.ExchangeDeclare("payment-exchange", ExchangeType.Direct);
            model.QueueDeclare("payment-queue", true, false, false, null);
            model.QueueBind("payment-queue", "payment-exchange", "payment.received");

            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                dynamic payment = JsonConvert.DeserializeObject(message);

                AccountManagementContext context = new AccountManagementContext();
                Accounts account = context.Accounts.Find(Convert.ToInt32(payment.AccountId.Value));

                account.Balance -= Convert.ToDecimal(payment.Amount.Value);
                context.SaveChanges();

                Console.WriteLine(message);
            };

            model.BasicConsume("payment-queue", true, consumer);
            Console.ReadLine();
            
        }
    }
}
