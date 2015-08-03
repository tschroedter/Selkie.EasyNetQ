using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Extensions
{
    public static class BusExtensions
    {
        private const int MaxDegreeOfParallelism = 10;
        internal static readonly Dictionary <string, object> Padlocks = new Dictionary <string, object>();

        private static readonly LimitedConcurrencyLevelTaskScheduler TaskScheduler =
            new LimitedConcurrencyLevelTaskScheduler(MaxDegreeOfParallelism);

        private static readonly TaskFactory Factory = new TaskFactory(TaskScheduler);

        [NotNull]
        internal static Task CreateTask <T>([NotNull] ILogger logger,
                                            [NotNull] Action <T> handler,
                                            [NotNull] T message,
                                            [NotNull] object padlock) where T : class
        {
            logger.Debug("Received '{0}' and creating task for it...".Inject(message.GetType()));

            Task task = Factory.StartNew(() =>
                                         {
                                             lock ( padlock )
                                             {
                                                 logger.Debug("Handling '{0}'...".Inject(message.GetType()));

                                                 handler(message);
                                             }
                                         },
                                         TaskCreationOptions.LongRunning);

            return task;
        }

        // ReSharper disable TooManyArguments
        public static void SubscribeHandlerAsync <T>([NotNull] this IBus bus,
                                                     [NotNull] ILogger logger,
                                                     [NotNull] string subscriptionId,
                                                     [NotNull] Action <T> handler) where T : class
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
            object padlock;

            if ( !Padlocks.TryGetValue(subscriptionId,
                                       out padlock) )
            {
                padlock = new object();

                Padlocks.Add(subscriptionId,
                             padlock);
            }

            return padlock;
        }

        // ReSharper restore TooManyArguments
    }
}