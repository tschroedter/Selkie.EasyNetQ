using System;
using System.Diagnostics.CodeAnalysis;
using Selkie.EasyNetQ.Examples.Message;

namespace Selkie.EasyNetQ.Examples.Consumer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class MessageBConsumerAsync : SelkieMessageConsumerAsync <MessageB>
    {
        public override void Handle(MessageB message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType()
                                     .Name);
        }
    }
}