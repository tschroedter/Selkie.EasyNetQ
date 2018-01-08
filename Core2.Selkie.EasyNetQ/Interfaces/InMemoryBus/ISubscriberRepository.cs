using System;
using System.Collections.Generic;
using Core2.Selkie.EasyNetQ.InMemoryBus;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus
{
    public interface ISubscriberRepository
    {
        [UsedImplicitly]
        IEnumerable <Type> Messages { get; }

        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>();

        void Unsubscribe <TMessage>([NotNull] string subscriptionId);
    }
}