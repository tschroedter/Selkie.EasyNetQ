using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using EasyNetQ;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class BusBuilder
    {
        public const string ConnectionString = "host=localhost;" +
                                               "virtualHost=selkie;" +
                                               "username=selkie;" +
                                               "password=selkie;" +
                                               "prefetchcount=100";

        [NotNull]
        public static IBus CreateMessageBus([NotNull] IWindsorContainer container)
        {
            IEasyNetQLogger logger = container.Resolve <ISelkieEasyNetQLogger>();

            IBus bus = RabbitHutch.CreateBus(ConnectionString,
                                             serviceRegister => serviceRegister.Register(_ => logger));

            return bus;
        }
    }
}