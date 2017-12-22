using System;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class SelkieInMemoryBus : ISelkieInMemoryBus
    {
        public SelkieInMemoryBus([NotNull] ISubscriberStore store,
                                 [NotNull] IMessageAggregator aggregator)
        {
            m_Store = store;
            m_Aggregator = aggregator;
        }

        private readonly IMessageAggregator m_Aggregator;
        private readonly object m_Padlock = new object();
        private readonly ISubscriberStore m_Store;

        public void Publish <T>(T message)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.Publish(message);
            }
        }

        public void PublishAsync <T>(T message)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.PublishAsync(message);
            }
        }

        public void Subscribe <T>(string subscriptionId,
                                  Action <T> handler)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.Subscribe(subscriptionId,
                                  handler);
            }
        }

        public void SubscribeAsync <T>(string subscriptionId,
                                       Action <T> handler)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.SubscribeAsync(subscriptionId,
                                       handler);
            }
        }
    }
}