using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus
{
    public interface IMessageAggregator
    {
        void Publish <T>([NotNull] T message);
        void PublishAsync <T>([NotNull] T message);
    }
}