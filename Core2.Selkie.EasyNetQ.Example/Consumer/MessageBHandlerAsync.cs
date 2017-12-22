using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.EasyNetQ;

namespace Selkie.EasyNetQ.Examples.Consumer
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