using System;
using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly Dictionary <Type, Dictionary <string, object>> m_Subscribers =
            new Dictionary <Type, Dictionary <string, object>>();

        public IEnumerable <Type> Messages => m_Subscribers.Keys.ToArray();

        public void Subscribe <TMessage>(string subscriptionId,
                                         Action <TMessage> handler)
        {
            if ( !m_Subscribers.ContainsKey(typeof( TMessage )) )
            {
                m_Subscribers.Add(typeof( TMessage ),
                                  new Dictionary <string, object>());
            }

            Dictionary <string, object> subscriptionIds = m_Subscribers [ typeof( TMessage ) ];

            if ( subscriptionIds.ContainsKey(subscriptionId) )
            {
                subscriptionIds.Remove(subscriptionId);
            }

            var info = new SubscriberInfo <TMessage>(subscriptionId,
                                                     handler);

            subscriptionIds.Add(subscriptionId,
                                info);
        }

        public void Unsubscribe <TMessage>(string subscriptionId)
        {
            if ( !m_Subscribers.ContainsKey(typeof( TMessage )) )
            {
                return;
            }

            Dictionary <string, object> subscriptionIds = m_Subscribers [ typeof( TMessage ) ];

            if ( subscriptionIds.ContainsKey(subscriptionId) )
            {
                subscriptionIds.Remove(subscriptionId);
            }

            if ( !subscriptionIds.Any() )
            {
                m_Subscribers.Remove(typeof( TMessage ));
            }
        }

        public IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>()
        {
            if ( !m_Subscribers.ContainsKey(typeof( TMessage )) )
            {
                return new SubscriberInfo <TMessage>[0];
            }

            Dictionary <string, object> subscriptions = m_Subscribers [ typeof( TMessage ) ];

            IEnumerable <SubscriberInfo <TMessage>> subscribers =
                subscriptions.Values.Cast <SubscriberInfo <TMessage>>();

            return subscribers;
        }

        [UsedImplicitly]
        internal IEnumerable <string> GetSubscriptionIdsForMessage <TMessage>()
        {
            if ( !m_Subscribers.ContainsKey(typeof( TMessage )) )
            {
                return new string[0];
            }

            Dictionary <string, object> subscriptionIds = m_Subscribers [ typeof( TMessage ) ];

            return subscriptionIds.Keys.ToArray();
        }
    }
}