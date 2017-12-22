using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core2.Selkie.Windsor.Interfaces;
using EasyNetQ;
using JetBrains.Annotations;

[assembly: InternalsVisibleTo("Core2.Selkie.EasyNetQ.Tests")]

namespace Core2.Selkie.EasyNetQ.Extensions
{
    public static class BusExtensions
    {
        private const int MaxDegreeOfParallelism = 10;

        [UsedImplicitly]
        internal static readonly Dictionary <string, object> Padlocks = new Dictionary <string, object>();

        private static readonly LimitedConcurrencyLevelTaskScheduler TaskScheduler =
            new LimitedConcurrencyLevelTaskScheduler(MaxDegreeOfParallelism);

        private static readonly TaskFactory Factory = new TaskFactory(TaskScheduler);

        [NotNull]
        public static Task CreateTask <T>([NotNull] ISelkieLogger logger,
                                          [NotNull] Action <T> handler,
                                          [NotNull] T message,
                                          [NotNull] object padlock)
        {
            logger.Debug($"Received '{message.GetType()}' and creating task for it...");

            Task task = Factory.StartNew(() =>
                                         {
                                             lock ( padlock )
                                             {
                                                 logger.Debug($"Handling '{message.GetType()}'...");

                                                 handler(message);
                                             }
                                         },
                                         TaskCreationOptions.None);

            return task;
        }

        [UsedImplicitly]
        public static void SubscribeHandlerAsync <T>([NotNull] this IBus bus,
                                                     [NotNull] ISelkieLogger logger,
                                                     [NotNull] string subscriptionId,
                                                     [NotNull] Action <T> handler)
            where T : class
        {
            object padlock = FindOrCreatePadlock(subscriptionId);

            Func <T, Task> func = message => CreateTask(logger,
                                                        handler,
                                                        message,
                                                        padlock);

            bus.SubscribeAsync(subscriptionId,
                               func);
        }

        internal static object FindOrCreatePadlock(string subscriptionId)
        {
            object m_padlock;

            if ( Padlocks.TryGetValue(subscriptionId,
                                      out m_padlock) )
            {
                return m_padlock;
            }

            m_padlock = new object();

            Padlocks.Add(subscriptionId,
                         m_padlock);

            return m_padlock;
        }
    }
}