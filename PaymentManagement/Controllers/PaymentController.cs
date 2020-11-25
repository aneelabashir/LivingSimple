using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentManagement.DataAccess;
using PaymentManagement.Model;
using Plain.RabbitMQ;
using RabbitMQ.Client;

namespace PaymentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        //STUDY EXCHANGES AND TYPES OF EXCHANGES
        //STUDY MASSTRANSIST on RabbitMQ
        //STUDY HOW DATA IS SHARED BETWEEN MICROSERVICES

        private PaymentManagementDbContext _dbContext;
        private ConnectionFactory _factory;
        private IReplyToPublisher _publisher;

        public PaymentController(PaymentManagementDbContext dbContext, ConnectionFactory factory, IReplyToPublisher publisher)
        {
            _dbContext = dbContext;
            _factory = factory;
            _publisher = publisher;
        }

        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return _dbContext.Payments.ToList();
        }

        [HttpPost]
        public int Post(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            //_dbContext.SaveChanges();

            string message = JsonConvert.SerializeObject(payment);
            var body = Encoding.UTF8.GetBytes(message);

            //IConnection connection = _factory.CreateConnection();
            //IModel model = connection.CreateModel();
            //model.ExchangeDeclare("payment-exchange", ExchangeType.Direct);
            ////model.QueueDeclare("payment-queue", true, false, false, null); 

            ////Header exchange settings, same way in consumer
            //IBasicProperties properties = model.CreateBasicProperties();
            //properties.Headers = new Dictionary<string, object> { { "MessageType", "payment-done" } };

            //model.BasicPublish("payment-exchange", "payment.received", properties, body);

          //  _publisher.Publish(message, "payment.received", null);

            return payment.Id;
        }

    }
}
