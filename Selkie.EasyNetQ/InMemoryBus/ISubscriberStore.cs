using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.InMemoryBus
{
    public interface ISubscriberStore
    {
        void SubscribeAsync <TMessage>([NotNull] string subscriptionId,
                                       [NotNull] Action <TMessage> handler);

        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        void Unsubscribe <TMessage>([NotNull] string subscriptionId);
        void UnsubscribeAsync <TMessage>([NotNull] string subscriptionId);
        IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>();
        IEnumerable <SubscriberInfo <TMessage>> SubscribersAsync <TMessage>();
    }
}