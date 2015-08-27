using System;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.InMemoryBus
{
    public class SubscriberInfo <TMessage>
    {
        public SubscriberInfo([NotNull] string subscriptionId,
                              Action <TMessage> handler)
        {
            SubscriptionId = subscriptionId;
            Handler = handler;
        }

        public string SubscriptionId { get; private set; }
        public Action <TMessage> Handler { get; private set; }
    }
}