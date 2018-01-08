using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces
{
    public interface ISelkieInMemoryBus : ISelkieBus
    {
        [UsedImplicitly]
        void Unsubscribe <T>(string subscriptionId)
            where T : class;

        [UsedImplicitly]
        void UnsubscribeAsync <T>(string subscriptionId)
            where T : class;
    }
}