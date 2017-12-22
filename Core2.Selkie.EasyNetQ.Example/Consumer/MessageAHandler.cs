using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Core2.Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.EasyNetQ;

namespace Core2.Selkie.EasyNetQ.Examples.Consumer
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class MessageAHandler : SelkieMessageHandler <MessageA>
    {
        [UsedImplicitly]
        public override void Handle(MessageA message)
        {
            string handler = this.GetType().Name;
            string messageFullName = message.GetType().FullName;

            Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
        }
    }
}