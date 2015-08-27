using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.EasyNetQ.InMemoryBus
{
    public interface ISubscriberRepositoryFactory : ITypedFactory
    {
        [NotNull]
        ISubscriberRepository Create();

        void Release([NotNull] ISubscriberRepository colony);
    }
}