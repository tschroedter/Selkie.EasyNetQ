using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.EasyNetQ.InMemoryBus;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.EasyNetQ.Tests.InMemoryBus
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SelkieInMemoryBusTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Subscribe_CallsSubscribe_WhenCalled([NotNull] ISubscriberStore store,
                                                        [NotNull] IMessageAggregator aggregator,
                                                        [NotNull] TestHandler handler)
        {
            // Arrange
            SelkieInMemoryBus sut = CreateSut(store,
                                              aggregator);

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Assert
            store.Received().Subscribe <TestMessage>("SubscriptionId",
                                                     handler.Handle);
        }

        [Theory]
        [AutoNSubstituteData]
        public void SubscribeAsync_CallsSubscribeAsync_WhenCalled([NotNull] ISubscriberStore store,
                                                                  [NotNull] IMessageAggregator aggregator,
                                                                  [NotNull] TestHandler handler)
        {
            // Arrange
            SelkieInMemoryBus sut = CreateSut(store,
                                              aggregator);

            // Act
            sut.SubscribeAsync <TestMessage>("SubscriptionId",
                                             handler.Handle);

            // Assert
            store.Received().SubscribeAsync <TestMessage>("SubscriptionId",
                                                          handler.Handle);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Publish_CallsPublish_WhenCalled([NotNull] ISubscriberStore store,
                                                    [NotNull] IMessageAggregator aggregator,
                                                    [NotNull] TestMessage message,
                                                    [NotNull] TestHandler handler)
        {
            // Arrange
            SelkieInMemoryBus sut = CreateSut(store,
                                              aggregator);

            // Act
            sut.Publish(message);

            // Assert
            aggregator.Received().Publish(message);
        }

        [Theory]
        [AutoNSubstituteData]
        public void PublishAsync_CallsPublishAsync_WhenCalled([NotNull] ISubscriberStore store,
                                                              [NotNull] IMessageAggregator aggregator,
                                                              [NotNull] TestMessage message,
                                                              [NotNull] TestHandler handler)
        {
            // Arrange
            SelkieInMemoryBus sut = CreateSut(store,
                                              aggregator);

            // Act
            sut.PublishAsync(message);

            // Assert
            aggregator.Received().PublishAsync(message);
        }

        private SelkieInMemoryBus CreateSut([NotNull] ISubscriberStore store,
                                            [NotNull] IMessageAggregator aggregator)
        {
            return new SelkieInMemoryBus(store,
                                         aggregator);
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