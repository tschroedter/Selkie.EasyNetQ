using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Extensions;
using Selkie.Windsor;

namespace Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class MessageAggregator : IMessageAggregator
    {
        private readonly ISelkieLogger m_Logger;
        private readonly ISubscriberStore m_Store;

        public MessageAggregator([NotNull] ISelkieLogger logger,
                                 [NotNull] ISubscriberStore store)
        {
            m_Logger = logger;
            m_Store = store;
        }

        internal bool IsCallAllHandlersSync { get; set; }

        public void Publish <T>(T message)
        {
            CallHandlers(message,
                         m_Store.Subscribers <T>());

            CallHandlersAsync(message,
                              m_Store.SubscribersAsync <T>());
        }

        public void PublishAsync <T>(T message)
        {
            CallHandlersAsync(message,
                              m_Store.Subscribers <T>());

            CallHandlersAsync(message,
                              m_Store.SubscribersAsync <T>());
        }

        private void CallHandlersAsync <T>([NotNull] T message,
                                           [NotNull] IEnumerable <SubscriberInfo <T>> information)
        {
            foreach ( SubscriberInfo <T> info in information )
            {
                object padlock = BusExtensions.FindOrCreatePadlock(info.SubscriptionId);

                Task task = BusExtensions.CreateTask(m_Logger,
                                                     info.Handler,
                                                     message,
                                                     padlock);

                if ( IsCallAllHandlersSync )
                {
                    task.Wait(5000);
                }
            }
        }

        private void CallHandlers <T>(T message,
                                      [NotNull] IEnumerable <SubscriberInfo <T>> information)
        {
            foreach ( SubscriberInfo <T> info in information )
            {
                object padlock = BusExtensions.FindOrCreatePadlock(info.SubscriptionId);

                lock ( padlock )
                {
                    info.Handler(message);
                }
            }
        }
    }
}