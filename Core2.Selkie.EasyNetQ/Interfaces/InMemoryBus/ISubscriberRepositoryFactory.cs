using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus
{
    public interface ISubscriberRepositoryFactory : ITypedFactory
    {
        [NotNull]
        ISubscriberRepository Create();

        [UsedImplicitly]
        void Release([NotNull] ISubscriberRepository colony);
    }
}