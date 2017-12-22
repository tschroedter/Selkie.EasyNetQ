using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Windsor;
using Core2.Selkie.EasyNetQ;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Examples.Messages;
using Core2.Selkie.Windsor.Interfaces;

namespace Selkie.EasyNetQ.Examples
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main()
        {
            var container = new WindsorContainer();
            var installer = new Installer();
            container.Install(installer);

            Assembly assembly = typeof( Installer ).Assembly;

            var consumers = container.Resolve <IRegisterMessageHandlers>();
            consumers.Register(container,
                               assembly);
            container.Release(consumers);

            var client = container.Resolve <ISelkieManagementClient>();
            client.CheckOrConfigureRabbitMq();

            var bus = container.Resolve <ISelkieBus>();

            var logger = container.Resolve <ISelkieLogger>();
            var subscriber = new TestSubscriber(logger,
                                                bus);

            subscriber.Subscribe();

            bus.Publish(new MessageA());
            bus.PublishAsync(new MessageB());

            var inMemoryBus = container.Resolve <ISelkieInMemoryBus>();
            var exampleSync = new InMemoryBusExampleSync(inMemoryBus);
            exampleSync.Run();

            var exampleASync = new InMemoryBusExampleASync(inMemoryBus);
            exampleASync.Run();
            container.Release(inMemoryBus);

            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}