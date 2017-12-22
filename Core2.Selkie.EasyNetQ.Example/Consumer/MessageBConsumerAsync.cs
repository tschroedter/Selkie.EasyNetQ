using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.EasyNetQ;

namespace Core2.Selkie.EasyNetQ.Examples.Consumer
{
    [ExcludeFromCodeCoverage]
    public class MessageBConsumerAsync : SelkieMessageConsumerAsync <MessageB>
    {
        public override void Handle(MessageB message)
        {
            string handler = this.GetType().Name;
            string messageFullName = message.GetType().FullName;

            Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
        }
    }
}