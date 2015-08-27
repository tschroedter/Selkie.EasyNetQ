using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.EasyNetQ.InMemoryBus;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.EasyNetQ.Tests.InMemoryBus.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SubscriberStoreTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void SubscribeAsync_CallsSubscribe_WhenCalled([NotNull] ISubscriberRepository sync,
                                                             [NotNull] ISubscriberRepository async,
                                                             [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.SubscribeAsync <TestMessage>("SubscriptionId",
                                             handler.Handle);

            // Assert
            async.Received().Subscribe <TestMessage>("SubscriptionId",
                                                     handler.Handle);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Subscribe_CallsSubscribe_WhenCalled([NotNull] ISubscriberRepository sync,
                                                        [NotNull] ISubscriberRepository async,
                                                        [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Assert
            sync.Received().Subscribe <TestMessage>("SubscriptionId",
                                                    handler.Handle);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Unsubscribe_CallsUnsubscribe_WhenCalled([NotNull] ISubscriberRepository sync,
                                                            [NotNull] ISubscriberRepository async,
                                                            [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            sync.Received().Unsubscribe <TestMessage>("SubscriptionId");
        }

        [Theory]
        [AutoNSubstituteData]
        public void UnsubscribeAsync_CallsUnsubscribe_WhenCalled([NotNull] ISubscriberRepository sync,
                                                                 [NotNull] ISubscriberRepository async,
                                                                 [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.UnsubscribeAsync <TestMessage>("SubscriptionId");

            // Assert
            async.Received().Unsubscribe <TestMessage>("SubscriptionId");
        }

        [Theory]
        [AutoNSubstituteData]
        public void Subscribers_CallsSubscribers_WhenCalled([NotNull] ISubscriberRepository sync,
                                                            [NotNull] ISubscriberRepository async,
                                                            [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.Subscribers <TestMessage>();

            // Assert
            sync.Received().Subscribers <TestMessage>();
        }

        [Theory]
        [AutoNSubstituteData]
        public void SubscribersAsync_CallsSubscribersAsync_WhenCalled([NotNull] ISubscriberRepository sync,
                                                                      [NotNull] ISubscriberRepository async,
                                                                      [NotNull] TestHandler handler)
        {
            // Arrange
            var factory = Substitute.For <ISubscriberRepositoryFactory>();
            factory.Create().Returns(sync,
                                     async);

            SubscriberStore sut = CreateSut(factory);

            // Act
            sut.SubscribersAsync <TestMessage>();

            // Assert
            async.Received().Subscribers <TestMessage>();
        }

        private SubscriberStore CreateSut([NotNull] ISubscriberRepositoryFactory factory)
        {
            return new SubscriberStore(factory);
        }

        public class TestHandler
        {
            public void Handle([NotNull] TestMessage message)
            {
            }
        }

        public class TestMessage
        {
        }
    }
}