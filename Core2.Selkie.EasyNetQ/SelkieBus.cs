using System;
using Core2.Selkie.EasyNetQ.Extensions;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using EasyNetQ;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class SelkieBus : ISelkieBus
    {
        public SelkieBus([NotNull] ISelkieLogger logger,
                         [NotNull] IBus bus)
        {
            m_Logger = logger;
            m_Bus = bus;
        }

        private readonly IBus m_Bus;
        private readonly ISelkieLogger m_Logger;

        public void Publish <T>(T message)
            where T : class
        {
            m_Bus.Publish(message);
        }

        public void PublishAsync <T>(T message)
            where T : class
        {
            m_Bus.PublishAsync(message);
        }

        public void Subscribe <T>(string subscriptionId,
                                  Action <T> handler)
            where T : class
        {
            m_Bus.Subscribe(subscriptionId,
                            handler);
        }

        public void SubscribeAsync <T>(string subscriptionId,
                                       Action <T> handler)
            where T : class
        {
            m_Bus.SubscribeHandlerAsync(m_Logger,
                                        subscriptionId,
                                        handler);
        }
    }
}