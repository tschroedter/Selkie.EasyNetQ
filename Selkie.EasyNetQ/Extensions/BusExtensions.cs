using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Extensions
{
    public static class BusExtensions
    {
        // ReSharper disable TooManyArguments
        public static void SubscribeHandlerAsync <T>([NotNull] this IBus bus,
                                                     [NotNull] ILogger logger,
                                                     [NotNull] string subscriptionId,
                                                     [NotNull] Action <T> handler) where T : class
        {
            Func <T, Task> func = message => CreateTask(logger,
                                                        handler,
                                                        message);

            bus.SubscribeAsync(subscriptionId,
                               func);
        }

        // ReSharper restore TooManyArguments
        [NotNull]
        internal static Task CreateTask <T>([NotNull] ILogger logger,
                                            [NotNull] Action <T> handler,
                                            [NotNull] T message) where T : class
        {
            logger.Debug("Received '{0}' and creating task for it...".Inject(message.GetType()));

            Task task = Task.Factory.StartNew(() =>
                                              {
                                                  logger.Debug("Handling '{0}'...".Inject(message.GetType()));

                                                  handler(message);
                                              });

            return task;
        }
    }
}