using System.Collections.Generic;
using System.Threading.Tasks;
using Core2.Selkie.EasyNetQ.Extensions;
using Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.InMemoryBus
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class MessageAggregator : IMessageAggregator
    {
        public MessageAggregator([NotNull] ISelkieLogger logger,
                                 [NotNull] ISubscriberStore store)
        {
            m_Logger = logger;
            m_Store = store;
        }

        [UsedImplicitly]
        internal bool IsCallAllHandlersSync { get; set; }

        private readonly ISelkieLogger m_Logger;
        private readonly ISubscriberStore m_Store;

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
    }
}