using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.RabbitMQ
{
    public interface IReplyToConsumer : IDisposable
    {
        void Consume(Func<string, IDictionary<string, object>, bool> callback);
    }
}
