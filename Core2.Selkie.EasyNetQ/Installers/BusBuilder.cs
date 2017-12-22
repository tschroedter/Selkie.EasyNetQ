﻿using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using EasyNetQ;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class BusBuilder
    {
        internal const string VirtualHost = "selkie";
        internal const string Username = "selkie";
        internal const string Password = "selkie";

        private const string ConnectionString = "host=localhost;" +
                                                "virtualHost=" + VirtualHost + ";" +
                                                "username=" + Username + ";" +
                                                "password=" + Password + ";" +
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