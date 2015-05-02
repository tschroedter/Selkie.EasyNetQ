using System;
using System.Diagnostics.CodeAnalysis;
using Selkie.EasyNetQ.Examples.Message;

namespace Selkie.EasyNetQ.Examples.Consumer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class MessageAConsumer : SelkieMessageConsumer <MessageA>
    {
        public override void Handle(MessageA message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType()
                                     .Name);
        }
    }
}