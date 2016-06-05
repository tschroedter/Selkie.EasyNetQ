using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Examples.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Examples.Consumer
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class MessageAHandler : SelkieMessageHandler <MessageA>
    {
        [UsedImplicitly]
        public override void Handle(MessageA message)
        {
            Console.WriteLine("{0} ==> Consumed message {1}...".Inject(GetType().Name,
                                                                       message.GetType().Name));
        }
    }
}