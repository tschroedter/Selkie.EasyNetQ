using System;
using System.Threading.Tasks;
using Core2.Selkie.NUnit.Extensions;
using Core2.Selkie.Windsor.Interfaces;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.EasyNetQ.Tests
{
    [TestFixture]
    internal sealed class SelkieBusTests
    {
        private SelkieBus CreateSut([NotNull] ISelkieLogger logger,
                                    [NotNull] IBus bus)
        {
            return new SelkieBus(logger,
                                 bus);
        }

        private void TestHandler([NotNull] TestMessage message)
        {
        }

        private class TestMessage
        {
        }

        [Test]
        [AutoNSubstituteData]
        public void Publish_CallsPublish_ForMessage([NotNull] ISelkieLogger logger,
                                                    [NotNull] IBus bus)
        {
            // Arrange
            var message = new TestMessage();
            SelkieBus sut = CreateSut(logger,
                                      bus);

            // Act
            sut.Publish(message);

            // Assert
            bus.Received().Publish(message);
        }

        [Test]
        [AutoNSubstituteData]
        public void PublishAsyncPublish_CallsPublishAsync_ForMessage([NotNull] ISelkieLogger logger,
                                                                     [NotNull] IBus bus)
        {
            // Arrange
            var message = new TestMessage();
            SelkieBus sut = CreateSut(logger,
                                      bus);

            // Act
            sut.PublishAsync(message);

            // Assert
            bus.Received().PublishAsync(message);
        }

        [Test]
        [AutoNSubstituteData]
        public void Subscribe_CallsSubscribe_ForMessage([NotNull] ISelkieLogger logger,
                                                        [NotNull] IBus bus)
        {
            // Arrange
            SelkieBus sut = CreateSut(logger,
                                      bus);

            // Act
            sut.Subscribe <TestMessage>("Id",
                                        TestHandler);

            // Assert
            bus.Received().Subscribe <TestMessage>("Id",
                                                   TestHandler);
        }

        [Test]
        [AutoNSubstituteData]
        public void SubscribeAsync_CallsSubscribe_ForMessage([NotNull] ISelkieLogger logger,
                                                             [NotNull] IBus bus)
        {
            // Arrange
            SelkieBus sut = CreateSut(logger,
                                      bus);

            // Act
            sut.SubscribeAsync <TestMessage>("Id",
                                             TestHandler);

            // Assert
            bus.Received().SubscribeAsync("Id",
                                          Arg.Any <Func <TestMessage, Task>>());
        }
    }
}