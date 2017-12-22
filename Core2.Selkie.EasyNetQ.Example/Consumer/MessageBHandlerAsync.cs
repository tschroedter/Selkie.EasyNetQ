using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Core2.Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.EasyNetQ;

namespace Core2.Selkie.EasyNetQ.Examples.Consumer
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class MessageBHandlerAsync : SelkieMessageHandlerAsync <MessageB>
    {
        [UsedImplicitly]
        public override void Handle(MessageB message)
        {
            string handler = this.GetType().Name;
            string messageFullName = message.GetType().FullName;

            Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
        }
    }
}