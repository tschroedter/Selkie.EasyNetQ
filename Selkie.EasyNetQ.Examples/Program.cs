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

            var register = new RegisterMessageConsumers();
            register.Register(container,
                              assembly);

            var bus = container.Resolve <IBus>();
            bus.Publish(new MessageA());
            bus.Publish(new MessageB());

            Console.ReadLine();

            Environment.Exit(0);
        }
    }
}