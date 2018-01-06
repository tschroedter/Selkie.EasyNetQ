using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.Example.Messages;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example.Consumer
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class MessageAConsumer : SelkieMessageConsumer <MessageA>
    {
        public override void Handle(MessageA message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType().Name);
        }
    }
}