using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ;
using Selkie.EasyNetQ.Examples.Messages;

namespace Selkie.EasyNetQ.Examples.Consumer
{
    [ExcludeFromCodeCoverage]
    public class MessageAConsumer : SelkieMessageConsumer <MessageA>
    {
        public override void Handle(MessageA message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType().Name);
        }
    }
}