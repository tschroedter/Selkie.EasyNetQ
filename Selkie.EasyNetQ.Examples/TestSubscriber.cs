using JetBrains.Annotations;
using Selkie.EasyNetQ.Examples.Messages;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ.Examples
{
    public class TestSubscriber
    {
        public TestSubscriber([NotNull] ISelkieLogger logger,
                              [NotNull] ISelkieBus bus)
        {
            m_Logger = logger;
            m_Bus = bus;
        }

        private readonly ISelkieBus m_Bus;
        private readonly ISelkieLogger m_Logger;

        public void Subscribe()
        {
            string subscriptionId = GetType().FullName;

            m_Bus.Subscribe <MessageA>(subscriptionId,
                                       AHandler);
            m_Bus.SubscribeAsync <MessageB>(subscriptionId,
                                            BHandler);
        }

        private void AHandler(MessageA message)
        {
            LogReceivedMessage(message);
        }

        private void BHandler(MessageB message)
        {
            LogReceivedMessage(message);
        }

        private void LogReceivedMessage(object message)
        {
            string text = "{0} ==> Consumed message {1}...".Inject(
                                                                   GetType().Name,
                                                                   message.GetType().Name);
            m_Logger.Info(text);
        }
    }
}