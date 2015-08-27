using JetBrains.Annotations;

namespace Selkie.EasyNetQ.InMemoryBus
{
    public interface IMessageAggregator
    {
        void Publish <T>([NotNull] T message);
        void PublishAsync <T>([NotNull] T message);
    }
}