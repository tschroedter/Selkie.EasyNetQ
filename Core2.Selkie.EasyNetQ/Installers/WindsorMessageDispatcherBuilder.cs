using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class WindsorMessageDispatcherBuilder
    {
        [NotNull]
        public static AutoSubscriber CreateMessageDispatcher([NotNull] IWindsorContainer container)
        {
            var bus = container.Resolve <IBus>();

            var autoSubscriber = new AutoSubscriber(bus,
                                                    "Selkie_AutoSubscriber_")
                                 {
                                     AutoSubscriberMessageDispatcher = new WindsorMessageDispatcher(container),
                                     GenerateSubscriptionId =
                                         subscriptionInfo =>
                                         "Selkie_AutoSubscriber_" + subscriptionInfo.ConcreteType.Name
                                 };

            return autoSubscriber;
        }
    }
}