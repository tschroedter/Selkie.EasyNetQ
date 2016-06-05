using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.InMemoryBus
{
    public interface ISubscriberStore
    {
        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        void SubscribeAsync <TMessage>([NotNull] string subscriptionId,
                                       [NotNull] Action <TMessage> handler);

        IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>();
        IEnumerable <SubscriberInfo <TMessage>> SubscribersAsync <TMessage>();

        void Unsubscribe <TMessage>([NotNull] string subscriptionId);
        void UnsubscribeAsync <TMessage>([NotNull] string subscriptionId);
    }
}