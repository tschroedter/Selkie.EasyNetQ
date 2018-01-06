using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.Example.Messages;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example.Consumer
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class MessageBConsumerAsync : SelkieMessageConsumerAsync <MessageB>
    {
        public override void Handle(MessageB message)
        {
            string handler = GetType().Name;
            string messageFullName = message.GetType().FullName;

            Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
        }
    }
}