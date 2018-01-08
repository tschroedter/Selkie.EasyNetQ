using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Core2.Selkie.EasyNetQ.Example.Messages;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Example
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
            exampleSync.Unsubscribe();
            container.Release(exampleSync);

            var exampleASync = new InMemoryBusExampleASync(inMemoryBus);
            exampleASync.Run();
            exampleASync.Unsubscribe();
            container.Release(exampleASync);

            container.Release(inMemoryBus);

            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}