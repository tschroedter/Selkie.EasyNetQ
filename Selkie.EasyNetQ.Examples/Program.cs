using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Windsor;
using EasyNetQ;
using Selkie.EasyNetQ.Examples.Message;

namespace Selkie.EasyNetQ.Examples
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Program
    {
        public static void Main()
        {
            var container = new WindsorContainer();
            var installer = new Installer();
            container.Install(installer);

            Assembly assembly = typeof ( Installer ).Assembly;

            var consumers = container.Resolve<IRegisterMessageConsumers>();
            consumers.Register(container,
                              assembly);
            container.Release(consumers);

            var client = container.Resolve <ISelkieManagementClient>();
            client.CheckOrConfigureRabbitMq();

            var bus = container.Resolve <IBus>();
            bus.Publish(new MessageA());
            bus.Publish(new MessageB());

            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}