using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Interfaces
{
    public interface ISelkieManagementClient
    {
        [UsedImplicitly]
        void CheckOrConfigureRabbitMq();

        [UsedImplicitly]
        void DeleteAllBindings();

        [UsedImplicitly]
        void DeleteAllExchange();

        [UsedImplicitly]
        void DeleteAllQueues();

        [UsedImplicitly]
        void DeleteAllQueues([NotNull] string name);

        [UsedImplicitly]
        void PurgeAllQueues();

        [UsedImplicitly]
        void PurgeAllQueues([NotNull] string name);

        [UsedImplicitly]
        void PurgeQueueForServiceAndMessage([NotNull] string name,
                                            [NotNull] string messageName);
    }
}