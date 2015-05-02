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
            WindsorContainer container = new WindsorContainer();
            Installer installer = new Installer();
            container.Install(installer);

            Assembly assembly = typeof ( Installer ).Assembly;

            RegisterMessageConsumers register = new RegisterMessageConsumers();
            register.Register(container,
                              assembly);

            IBus bus = container.Resolve <IBus>();
            bus.Publish(new MessageA());
            bus.Publish(new MessageB());

            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}