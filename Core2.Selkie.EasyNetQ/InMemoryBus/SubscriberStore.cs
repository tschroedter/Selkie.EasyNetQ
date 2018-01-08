using System;
using System.Collections.Generic;
using Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Singleton)]
    [UsedImplicitly]
    public class SubscriberStore : ISubscriberStore
    {
        public SubscriberStore([NotNull] ISubscriberRepositoryFactory factory)
        {
            m_Repository = factory.Create();
            m_AsyncRepository = factory.Create();
        }

        private readonly ISubscriberRepository m_AsyncRepository;
        private readonly ISubscriberRepository m_Repository;

        public void SubscribeAsync <TMessage>(string subscriptionId,
                                              Action <TMessage> handler)
        {
            m_AsyncRepository.Subscribe(subscriptionId,
                                        handler);
        }

        public void Subscribe <TMessage>(string subscriptionId,
                                         Action <TMessage> handler)
        {
            m_Repository.Subscribe(subscriptionId,
                                   handler);
        }

        public void Unsubscribe <TMessage>(string subscriptionId)
        {
            m_Repository.Unsubscribe <TMessage>(subscriptionId);
        }

        public void UnsubscribeAsync <TMessage>(string subscriptionId)
        {
            m_AsyncRepository.Unsubscribe <TMessage>(subscriptionId);
        }

        public IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>()
        {
            return m_Repository.Subscribers <TMessage>();
        }

        public IEnumerable <SubscriberInfo <TMessage>> SubscribersAsync <TMessage>()
        {
            return m_AsyncRepository.Subscribers <TMessage>();
        }
    }
}