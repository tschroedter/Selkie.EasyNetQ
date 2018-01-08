using System;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces
{
    public interface ISelkieBus
    {
        [UsedImplicitly]
        void Publish <T>(T message)
            where T : class;

        [UsedImplicitly]
        void PublishAsync <T>(T message)
            where T : class;

        [UsedImplicitly]
        void Subscribe <T>([NotNull] string subscriptionId,
                           [NotNull] Action <T> handler)
            where T : class;

        [UsedImplicitly]
        void SubscribeAsync <T>([NotNull] string subscriptionId,
                                [NotNull] Action <T> handler)
            where T : class;
    }
}