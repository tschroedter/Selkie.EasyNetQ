using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Core2.Selkie.EasyNetQ.Example.Messages;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example.Consumer
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class MessageBConsumerAsync : IConsumeAsync<MessageB>
    {
        public Task Consume(MessageB message)
        {
            return new Task(() =>
                            {
                                string handler = GetType().Name;
                                string messageFullName = message.GetType().FullName;

                                Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
                            });
        }
    }
}