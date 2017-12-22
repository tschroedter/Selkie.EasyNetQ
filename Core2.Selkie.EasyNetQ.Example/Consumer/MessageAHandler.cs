using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.EasyNetQ;

namespace Selkie.EasyNetQ.Examples.Consumer
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