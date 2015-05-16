using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class RegisterMessageConsumers
    {
        private ILogger m_Logger;

        public void Register([NotNull] WindsorContainer container,
                             [NotNull] Assembly assembly)
        {
            m_Logger = container.Resolve <ILogger>();

            container.Install()
                     .Register(
                               Classes.FromAssembly(assembly)
                                      .Where(IsMessageConsumerLogged)
                                      .WithServiceSelf()
                                      .LifestyleTransient());

            var autoSubscriber = container.Resolve <AutoSubscriber>();
            autoSubscriber.Subscribe(assembly);
            autoSubscriber.SubscribeAsync(assembly);

            container.Release(m_Logger);
        }

        private bool IsMessageConsumer(Type x)
        {
            string name = x.Name;

            return name.EndsWith("ConsumerAsync") || name.EndsWith("Consumer");
        }

        private bool IsMessageConsumerLogged(Type x)
        {
            bool isCosumer = IsMessageConsumer(x);

            if ( isCosumer )
            {
                m_Logger.Info("Message Consumer: Registered {0}.".Inject(x.FullName));
            }

            return isCosumer;
        }
    }
}