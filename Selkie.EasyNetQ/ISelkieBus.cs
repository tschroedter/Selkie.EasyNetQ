using System;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    public interface ISelkieBus
    {
        void Publish <T>(T message)
            where T : class;

        void PublishAsync <T>(T message)
            where T : class;

        void Subscribe <T>([NotNull] string subscriptionId,
                           [NotNull] Action <T> handler)
            where T : class;

        void SubscribeAsync <T>([NotNull] string subscriptionId,
                                [NotNull] Action <T> handler)
            where T : class;
    }
}