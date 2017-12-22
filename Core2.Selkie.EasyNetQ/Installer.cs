using System;
using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.EasyNetQ.Management.Client;
using Core2.Selkie.EasyNetQ.Installers;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.",
                                           StringComparison.Ordinal);
        }

        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            var busInstaller = new BusInstaller();
            busInstaller.Install(container,
                                 store);

            var dispatcherBuilder = new WindsorMessageDispatcherInstaller();
            dispatcherBuilder.Install(container,
                                      store);

            container.Register(Component.For <ManagementClient>()
                                        .UsingFactoryMethod(ManagementClientLoaderBuilder.CreateLoader)
                                        .LifestyleTransient());
        }
    }
}