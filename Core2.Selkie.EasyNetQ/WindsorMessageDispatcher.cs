using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Castle.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    public class WindsorMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        public WindsorMessageDispatcher([NotNull] IWindsorContainer container)
        {
            m_Container = container;
            m_Logger = container.Resolve <ISelkieLogger>();
        }

        private readonly IWindsorContainer m_Container;
        private readonly ISelkieLogger m_Logger;

        public void Dispatch <TMessage, TConsumer>([NotNull] TMessage message) where TMessage : class
            where TConsumer : IConsume <TMessage>
        {
            var consumer = m_Container.Resolve <TConsumer>();
            try
            {
                consumer.Consume(message);
            }
            catch ( Exception exception )
            {
                m_Logger.Error("Message Dispatch Error",
                               exception);

                throw;
            }
            finally
            {
                m_Container.Release(consumer);
            }
        }

        public Task DispatchAsync <TMessage, TConsumer>([NotNull] TMessage message) where TMessage : class
            where TConsumer : IConsumeAsync <TMessage>
        {
            var consumer = m_Container.Resolve <TConsumer>();
            return consumer.Consume(message).ContinueWith(t => m_Container.Release(consumer));
        }
    }
}