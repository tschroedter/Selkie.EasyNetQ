using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
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
            autoSubscriber.GenerateSubscriptionId = GenerateSubscriptionId;
            autoSubscriber.Subscribe(assembly);
            autoSubscriber.SubscribeAsync(assembly);

            container.Release(m_Logger);
        }

        private string GenerateSubscriptionId(AutoSubscriberConsumerInfo arg)
        {
            string id = "AutoSubscriber_" + arg.ConcreteType.FullName;

            return id;
        }

        private bool IsMessageConsumerLogged(Type type)
        {
            bool isCosumer = IsMessageHandler(type);

            if ( isCosumer )
            {
                m_Logger.Info($"Message Consumer: Registered {type.FullName}.");
            }
            else
            {
                m_Logger.Debug($"Message Consumer: Ignored {type.FullName}.");
            }

            return isCosumer;
        }

        private bool IsMessageHandler(Type type)
        {
            string name = type.Name;

            return name.EndsWith("HandlerAsync") || name.EndsWith("Handler");
        }
    }
}