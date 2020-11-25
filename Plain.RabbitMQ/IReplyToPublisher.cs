using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.RabbitMQ
{
    public interface IReplyToPublisher : IDisposable
    {
        object Publish(string message, string routingKey, IDictionary<string, object> messageAttributes, Func<string, IDictionary<string, object>, bool> callback, string timeToLive = null);
    }
}
