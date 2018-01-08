using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Windsor;
using Core2.Selkie.EasyNetQ.Example.Messages;
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

            Assembly assembly = typeof( Installer ).Assembly;

            var handlers = container.Resolve <IRegisterMessageHandlers>();
            handlers.Register(container,
                              assembly);
            container.Release(handlers);

            // Todo AutoSubscriber not working
            /*
            var ibus = container.Resolve<IBus>();
            AutoSubscriber autoSubscriber = new AutoSubscriber(ibus, "AutoSubscriber");
            autoSubscriber.Subscribe(assembly);
            container.Release(ibus);
            */

            // work-around
            var consumers = container.Resolve <IRegisterMessageConsumers>();
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