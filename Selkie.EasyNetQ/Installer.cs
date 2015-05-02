using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
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

            BusInstaller busInstaller = new BusInstaller();
            busInstaller.Install(container,
                                 store);

            WindsorMessageDispatcherInstaller dispatcherBuilder = new WindsorMessageDispatcherInstaller();
            dispatcherBuilder.Install(container,
                                      store);
        }
    }
}