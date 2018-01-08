using System;
using System.Collections.Generic;
using Core2.Selkie.EasyNetQ.InMemoryBus;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus
{
    public interface ISubscriberStore
    {
        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        void SubscribeAsync <TMessage>([NotNull] string subscriptionId,
                                       [NotNull] Action <TMessage> handler);

        IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>();
        IEnumerable <SubscriberInfo <TMessage>> SubscribersAsync <TMessage>();

        [UsedImplicitly]
        void Unsubscribe <TMessage>([NotNull] string subscriptionId);

        [UsedImplicitly]
        void UnsubscribeAsync <TMessage>([NotNull] string subscriptionId);
    }
}