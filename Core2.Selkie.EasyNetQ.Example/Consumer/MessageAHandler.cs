using System;
using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.Example.Messages;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example.Consumer
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class MessageAHandler : SelkieMessageHandler <MessageA>
    {
        [UsedImplicitly]
        public override void Handle(MessageA message)
        {
            string handler = GetType().Name;
            string messageFullName = message.GetType().FullName;

            Console.WriteLine($"{handler} ==> Consumed message {messageFullName}...");
        }
    }
}