using System.Diagnostics.CodeAnalysis;
using Core2.Selkie.EasyNetQ.InMemoryBus;
using Core2.Selkie.EasyNetQ.Interfaces.InMemoryBus;
using Core2.Selkie.NUnit.Extensions;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.EasyNetQ.Tests.InMemoryBus
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class SubscriberStoreTests
    {
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
    }
}