using System;
using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieInMemoryBus : ISelkieInMemoryBus
    {
        private readonly IMessageAggregator m_Aggregator;
        private readonly object m_Padlock = new object();
        private readonly ISubscriberStore m_Store;

        public SelkieInMemoryBus([NotNull] ISubscriberStore store,
                                 [NotNull] IMessageAggregator aggregator)
        {
            m_Store = store;
            m_Aggregator = aggregator;
        }

        public void Publish <T>(T message) where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.Publish(message);
            }
        }

        public void PublishAsync <T>(T message) where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.PublishAsync(message);
            }
        }

        public void Subscribe <T>(string subscriptionId,
                                  Action <T> handler) where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.Subscribe(subscriptionId,
                                  handler);
            }
        }

        public void SubscribeAsync <T>(string subscriptionId,
                                       Action <T> handler) where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.SubscribeAsync(subscriptionId,
                                       handler);
            }
        }
    }
}