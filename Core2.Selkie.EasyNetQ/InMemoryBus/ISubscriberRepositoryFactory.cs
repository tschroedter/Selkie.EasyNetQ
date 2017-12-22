using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    public interface ISubscriberRepositoryFactory : ITypedFactory
    {
        [NotNull]
        ISubscriberRepository Create();

        void Release([NotNull] ISubscriberRepository colony);
    }
}