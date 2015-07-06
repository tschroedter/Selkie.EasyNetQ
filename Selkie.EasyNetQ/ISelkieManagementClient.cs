using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    public interface ISelkieManagementClient
    {
        void DeleteAllQueues();
        void DeleteAllQueues([NotNull] string name);
        void PurgeAllQueues();
        void PurgeAllQueues([NotNull] string name);
        void DeleteAllBindings();
        void DeleteAllExchange();

        void PurgeQueueForServiceAndMessage([NotNull] string name,
                                            [NotNull] string messageName);
    }
}