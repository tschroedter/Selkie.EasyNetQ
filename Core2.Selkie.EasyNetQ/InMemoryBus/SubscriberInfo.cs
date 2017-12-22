using System;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    public class SubscriberInfo <TMessage>
    {
        public SubscriberInfo([NotNull] string subscriptionId,
                              Action <TMessage> handler)
        {
            SubscriptionId = subscriptionId;
            Handler = handler;
        }

        public string SubscriptionId { get; }
        public Action <TMessage> Handler { get; }
    }
}