﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class WindsorMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IWindsorContainer m_Container;
        private readonly ILogger m_Logger;

        public WindsorMessageDispatcher([NotNull] IWindsorContainer container)
        {
            m_Container = container;
            m_Logger = container.Resolve <ILogger>();
        }

        public void Dispatch<TMessage, TConsumer>([NotNull] TMessage message)
            where TMessage : class
            where TConsumer : IConsume <TMessage>
        {
            TConsumer consumer = m_Container.Resolve <TConsumer>();
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

        public Task DispatchAsync<TMessage, TConsumer>([NotNull] TMessage message)
            where TMessage : class
            where TConsumer : IConsumeAsync <TMessage>
        {
            TConsumer consumer = m_Container.Resolve <TConsumer>();
            return consumer.Consume(message)
                           .ContinueWith(t => m_Container.Release(consumer));
        }
    }
}