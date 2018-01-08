using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.Example.Messages;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example.Consumer
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class MessageAConsumer : IConsume <MessageA>
    {
        public void Consume(MessageA message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType().Name);
        }
    }
}