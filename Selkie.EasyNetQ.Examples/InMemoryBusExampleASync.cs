using System;
using System.Linq;
using Selkie.EasyNetQ.Examples.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Examples
{
    public class InMemoryBusExampleASync
    {
        private readonly ISelkieInMemoryBus m_InMemoryBus;
        private readonly int[] m_TestSync = new int[100];
        private int m_IndexSync;

        public InMemoryBusExampleASync(ISelkieInMemoryBus inMemoryBus)
        {
            m_InMemoryBus = inMemoryBus;

            m_InMemoryBus.SubscribeAsync <MessageA>("one",
                                                    AHandlerOne);

            m_InMemoryBus.SubscribeAsync <MessageA>("two",
                                                    AHandlerTwo);
        }

        public void Run()
        {
            for ( var i = 0 ; i < m_TestSync.Length ; i++ )
            {
                m_InMemoryBus.PublishAsync(new MessageA());
            }

            while ( m_TestSync.Any(x => x == 0) )
            {
                Console.WriteLine("Async Waiting...");
            }

            Console.WriteLine("Async...all good!");
        }

        private void AHandlerOne(MessageA message)
        {
            int index = m_IndexSync;

            string text = "*** Async [One] Handling {0}...".Inject(message.GetType()
                                                                          .FullName);

            Console.WriteLine(text);

            m_TestSync [ index ] = 1;

            m_IndexSync++;
        }

        private void AHandlerTwo(MessageA message)
        {
            string text = "*** Async[Two] Handling {0}...".Inject(message.GetType()
                                                                         .FullName);

            Console.WriteLine(text);
        }
    }
}