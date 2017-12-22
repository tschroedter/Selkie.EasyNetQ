using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    public class WindsorMessageDispatcherInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] [UsedImplicitly] IConfigurationStore store)
        {
            container.Register(
                               Component.For <AutoSubscriber>()
                                        .UsingFactoryMethod(
                                                            () =>
                                                                WindsorMessageDispatcherBuilder
                                                                    .CreateMessageDispatcher(
                                                                                             container))
                                        .LifestyleSingleton());
        }
    }
}