using System;
using System.Diagnostics.CodeAnalysis;
using Selkie.EasyNetQ.Examples.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Examples.Consumer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class MessageAHandler : SelkieMessageHandler <MessageA>
    {
        public override void Handle(MessageA message)
        {
            Console.WriteLine("{0} ==> Consumed message {1}...".Inject(GetType().Name,
                                                                       message.GetType().Name));
        }
    }
}