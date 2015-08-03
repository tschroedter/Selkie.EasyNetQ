using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ.Management.Client;
using Selkie.EasyNetQ.Installers;
using Selkie.Windsor;

namespace Selkie.EasyNetQ
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class Installer : BaseInstaller <Installer>
    {
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

            // ReSharper disable MaximumChainedReferences
            container.Register(Component.For <ManagementClient>()
                                        .UsingFactoryMethod(ManagementClientLoaderBuilder.CreateLoader)
                                        .LifestyleTransient());
            // ReSharper restore MaximumChainedReferences
        }
    }
}