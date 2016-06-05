using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.EasyNetQ.InMemoryBus;
using Selkie.NUnit.Extensions;
using Selkie.Windsor;

namespace Selkie.EasyNetQ.Tests.InMemoryBus
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class MessageAggregatorTests
    {
        private MessageAggregator CreateSut(ISubscriberStore store)
        {
            var sut = new MessageAggregator(Substitute.For <ISelkieLogger>(),
                                            store)
                      {
                          IsCallAllHandlersSync = true
                      };

            return sut;
        }

        public class TestHandler
        {
            public bool WasCalled { get; set; }

            public void Handle([NotNull] TestMessage message)
            {
                WasCalled = true;
            }
        }

        public class TestMessage
        {
        }

        [Test]
        [AutoNSubstituteData]
        public void Publish_CallsActionsForASync_WhenCalled([NotNull] TestHandler one,
                                                            [NotNull] TestHandler two)
        {
            // Arrange
            var handlers = new[]
                           {
                               new SubscriberInfo <TestMessage>("one",
                                                                one.Handle),
                               new SubscriberInfo <TestMessage>("two",
                                                                two.Handle)
                           };

            var store = Substitute.For <ISubscriberStore>();
            store.SubscribersAsync <TestMessage>().Returns(handlers);
            MessageAggregator sut = CreateSut(store);

            // Act
            sut.Publish(new TestMessage());

            // Assert
            Assert.True(one.WasCalled,
                        "one.WasCalled");
            Assert.True(two.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        [AutoNSubstituteData]
        public void Publish_CallsActionsForSync_WhenCalled([NotNull] TestHandler one,
                                                           [NotNull] TestHandler two)
        {
            // Arrange
            var handlers = new[]
                           {
                               new SubscriberInfo <TestMessage>("one",
                                                                one.Handle),
                               new SubscriberInfo <TestMessage>("two",
                                                                two.Handle)
                           };

            var store = Substitute.For <ISubscriberStore>();
            store.Subscribers <TestMessage>().Returns(handlers);
            MessageAggregator sut = CreateSut(store);

            // Act
            sut.Publish(new TestMessage());

            // Assert
            Assert.True(one.WasCalled,
                        "one.WasCalled");
            Assert.True(two.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        [AutoNSubstituteData]
        public void PublishAsync_CallsActionsForASync_WhenCalled([NotNull] TestHandler one,
                                                                 [NotNull] TestHandler two)
        {
            // Arrange
            var handlers = new[]
                           {
                               new SubscriberInfo <TestMessage>("one",
                                                                one.Handle),
                               new SubscriberInfo <TestMessage>("two",
                                                                two.Handle)
                           };

            var store = Substitute.For <ISubscriberStore>();
            store.SubscribersAsync <TestMessage>().Returns(handlers);
            MessageAggregator sut = CreateSut(store);

            // Act
            sut.PublishAsync(new TestMessage());

            // Assert
            Assert.True(one.WasCalled,
                        "one.WasCalled");
            Assert.True(two.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        [AutoNSubstituteData]
        public void PublishAsync_CallsActionsForSync_WhenCalled([NotNull] TestHandler one,
                                                                [NotNull] TestHandler two)
        {
            // Arrange
            var handlers = new[]
                           {
                               new SubscriberInfo <TestMessage>("one",
                                                                one.Handle),
                               new SubscriberInfo <TestMessage>("two",
                                                                two.Handle)
                           };

            var store = Substitute.For <ISubscriberStore>();
            store.Subscribers <TestMessage>().Returns(handlers);
            MessageAggregator sut = CreateSut(store);

            // Act
            sut.PublishAsync(new TestMessage());

            // Assert
            Assert.True(one.WasCalled,
                        "one.WasCalled");
            Assert.True(two.WasCalled,
                        "two.WasCalled");
        }
    }
}