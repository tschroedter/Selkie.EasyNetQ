using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    [ProjectComponent(Lifestyle.Transient)]
    public class RegisterMessageHandlers : IRegisterMessageHandlers
    {
        private ISelkieLogger m_Logger;

        public void Register(IWindsorContainer container,
                             Assembly assembly)
        {
            m_Logger = container.Resolve <ISelkieLogger>();

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

        private bool IsMessageHandler(Type type)
        {
            string name = type.Name;

            return name.EndsWith("HandlerAsync") || name.EndsWith("Handler");
        }

        private bool IsMessageConsumerLogged(Type type)
        {
            bool isCosumer = IsMessageHandler(type);

            if ( isCosumer )
            {
                m_Logger.Info("Message Consumer: Registered {0}.".Inject(type.FullName));
            }

            return isCosumer;
        }
    }
}