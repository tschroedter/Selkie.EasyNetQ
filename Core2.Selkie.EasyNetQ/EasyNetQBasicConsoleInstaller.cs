using System.Reflection;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.EasyNetQ.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [UsedImplicitly]
    public abstract class EasyNetQBasicConsoleInstaller : Windsor.BasicConsoleInstaller
    {
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            var handlers = container.Resolve<IRegisterMessageHandlers>();
            var consumers = container.Resolve<IRegisterMessageConsumers>();

            foreach ( Assembly assembly in AllAssemblies )
            {
                handlers.Register(container,
                                  assembly);
                consumers.Register(container,
                                   assembly);
            }
            container.Release(handlers);
            container.Release(consumers);

            // Todo AutoSubscriber not working
            /*
            var ibus = container.Resolve<IBus>();
            AutoSubscriber autoSubscriber = new AutoSubscriber(ibus, "AutoSubscriber");
            autoSubscriber.Subscribe(assembly);
            container.Release(ibus);
            */
        }
    }
}