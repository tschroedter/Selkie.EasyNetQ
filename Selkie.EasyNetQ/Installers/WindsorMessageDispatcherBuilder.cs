﻿using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ.Installers
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class WindsorMessageDispatcherBuilder
    {
        [NotNull]
        public static AutoSubscriber CreateMessageDispatcher([NotNull] IWindsorContainer container)
        {
            IBus bus = container.Resolve <IBus>();

            AutoSubscriber autoSubscriber = new AutoSubscriber(bus,
                                                               "Selkie_AutoSubscriber_")
                                            {
                                                AutoSubscriberMessageDispatcher =
                                                    new WindsorMessageDispatcher(container),
                                                GenerateSubscriptionId =
                                                    subscriptionInfo =>
                                                    "Selkie_AutoSubscriber_" + subscriptionInfo.ConcreteType.Name
                                            };

            return autoSubscriber;
        }
    }
}