using System.Linq;
using Core2.Selkie.EasyNetQ.Example.Messages;
using Core2.Selkie.EasyNetQ.Interfaces;
using static System.Console;

namespace Core2.Selkie.EasyNetQ.Example
{
    public class InMemoryBusExampleASync
    {
        public InMemoryBusExampleASync(ISelkieInMemoryBus inMemoryBus)
        {
            m_InMemoryBus = inMemoryBus;

            m_InMemoryBus.SubscribeAsync <MessageA>("one",
                                                    AHandlerOne);

            m_InMemoryBus.SubscribeAsync <MessageA>("two",
                                                    AHandlerTwo);
        }

        private readonly ISelkieInMemoryBus m_InMemoryBus;
        private readonly int[] m_TestSync = new int[100];
        private int m_IndexSync;

        public void Run()
        {
            for ( var i = 0 ; i < m_TestSync.Length ; i++ )
            {
                m_InMemoryBus.PublishAsync(new MessageA());

                WriteLine($"Sending MessageA {i} time!");
            }

            while ( m_TestSync.Any(x => x == 0) )
            {
                WriteLine("Async Waiting...");
            }

            WriteLine("Async...all good!");
        }

        public void Unsubscribe()
        {
            m_InMemoryBus.Unsubscribe <MessageA>("one");
            m_InMemoryBus.UnsubscribeAsync <MessageA>("one");
        }

        private void AHandlerOne(MessageA message)
        {
            int index = m_IndexSync;

            string fullName = message.GetType().FullName;

            string text = $"*** Async [One] Handling {fullName}...";

            WriteLine(text);

            m_TestSync [ index ] = 1;

            m_IndexSync++;
        }

        private void AHandlerTwo(MessageA message)
        {
            string fullName = message.GetType().FullName;

            string text = $"*** Async[Two] Handling {fullName}...";

            WriteLine(text);
        }

        ~InMemoryBusExampleASync()
        {
            Unsubscribe();
        }
    }
}