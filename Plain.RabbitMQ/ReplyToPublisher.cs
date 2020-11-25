using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.RabbitMQ
{
    public class ReplyToPublisher : IReplyToPublisher
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly string _exchange;
        private readonly IModel _model;
        private bool _disposed;
        private readonly string _replyToQueue;

        public ReplyToPublisher(IConnectionProvider connectionProvider, string exchange, string exchangeType,
              int timeToLive = 30000)
        {
            _connectionProvider = connectionProvider;
            _exchange = exchange;
            //_replyToQueue = replyToQueue;
            _model = _connectionProvider.GetConnection().CreateModel();
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", timeToLive }
            };
            _model.ExchangeDeclare(_exchange, exchangeType, arguments: ttl);

            _replyToQueue = _model.QueueDeclare().QueueName;
        }

        public object Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, Func<string, IDictionary<string, object>, bool> callback, string timeToLive = "30000")
        {
            object returnObj = null;
            string correlationId = Guid.NewGuid().ToString();
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _model.CreateBasicProperties();
            properties.Persistent = true;
            properties.Headers = messageAttributes;
            properties.Expiration = timeToLive;
            properties.CorrelationId = correlationId;
            properties.ReplyTo = _replyToQueue;

            EventingBasicConsumer consumer = new EventingBasicConsumer(_model);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);

                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    returnObj = callback.Invoke(message, null);
                }
            };

            _model.BasicPublish(_exchange, routingKey, properties, body);

            _model.BasicConsume(consumer, _replyToQueue, true);

            return returnObj;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _model?.Close();

            _disposed = true;
        }
    }
}
