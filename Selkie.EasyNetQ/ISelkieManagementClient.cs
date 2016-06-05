using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    public interface ISelkieManagementClient
    {
        void CheckOrConfigureRabbitMq();
        void DeleteAllBindings();
        void DeleteAllExchange();
        void DeleteAllQueues();
        void DeleteAllQueues([NotNull] string name);
        void PurgeAllQueues();
        void PurgeAllQueues([NotNull] string name);

        void PurgeQueueForServiceAndMessage([NotNull] string name,
                                            [NotNull] string messageName);
    }
}