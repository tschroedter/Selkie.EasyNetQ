using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.Windsor;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.EasyNetQ.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SelkieBusTests
    {
        [Theory]
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

        [Theory]
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

        [Theory]
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

        [Theory]
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
    }
}