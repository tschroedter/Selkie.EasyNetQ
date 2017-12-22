using System;
using System.Linq;
using Core2.Selkie.EasyNetQ;
using Selkie.EasyNetQ.Examples.Messages;

namespace Selkie.EasyNetQ.Examples
{
    public class InMemoryBusExampleSync
    {
        public InMemoryBusExampleSync(ISelkieInMemoryBus inMemoryBus)
        {
            m_InMemoryBus = inMemoryBus;

            m_InMemoryBus.Subscribe <MessageA>("one",
                                               AHandlerOne);

            m_InMemoryBus.Subscribe <MessageA>("two",
                                               AHandlerTwo);
        }

        private readonly ISelkieInMemoryBus m_InMemoryBus;
        private readonly int[] m_TestSync = new int[100];
        private int m_IndexSync;

        public void Run()
        {
            for ( var i = 0 ; i < m_TestSync.Length ; i++ )
            {
                m_InMemoryBus.Publish(new MessageA());
            }

            while ( m_TestSync.Any(x => x == 0) )
            {
                Console.WriteLine("Sync Waiting...");
            }

            Console.WriteLine("Sync...all good!");
        }

        private void AHandlerOne(MessageA message)
        {
            int index = m_IndexSync;
            string fullName = message.GetType().FullName;

            string text = $"*** Sync [One] Handling {fullName}...";

            Console.WriteLine(text);

            m_TestSync [ index ] = 1;

            m_IndexSync++;
        }

        private void AHandlerTwo(MessageA message)
        {
            string fullName = message.GetType().FullName;

            string text = $"*** Sync [Two] Handling {0}...";

            Console.WriteLine(text);
        }
    }
}