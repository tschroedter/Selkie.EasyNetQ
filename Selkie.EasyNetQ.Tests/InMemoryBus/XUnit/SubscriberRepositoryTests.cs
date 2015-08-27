using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Selkie.EasyNetQ.InMemoryBus;
using Xunit;

namespace Selkie.EasyNetQ.Tests.InMemoryBus.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SubscriberRepositoryTests
    {
        [Fact]
        public void Subscribe_AddsMessageToMessages_WhenCalled()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Assert
            Assert.True(sut.Messages.Contains(typeof ( TestMessage )));
        }

        [Fact]
        public void Subscribe_AddsMessageToMessages_ForSameMessageRegistraion()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();

            // Act
            sut.Subscribe <TestMessage>("Test 1",
                                        handler.Handle);
            sut.Subscribe <TestMessage>("Test 2",
                                        handler.Handle);

            // Assert
            Assert.True(sut.Messages.Contains(typeof ( TestMessage )));
        }

        [Fact]
        public void Subscribers_ReturnsHandlers_ForMessage()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();

            sut.Subscribe <TestMessage>("Test 1",
                                        handler.Handle);
            sut.Subscribe <TestMessage>("Test 2",
                                        handler.Handle);

            // Act
            IEnumerable <SubscriberInfo <TestMessage>> actual = sut.Subscribers <TestMessage>().ToArray();

            // Assert
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void Subscribers_ReturnsEmpty_ForUnknownMessage()
        {
            // Arrange
            SubscriberRepository sut = CreateSut();

            // Act
            IEnumerable <SubscriberInfo <TestMessage>> actual = sut.Subscribers <TestMessage>();

            // Assert
            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void Subscribe_AddsIdToIds_WhenCalled()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Assert
            IEnumerable <string> actual = sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(actual.Contains("SubscriptionId"));
        }

        [Fact]
        public void Subscribe_AddsIdsToIds_WhenCalled()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId 1",
                                        handler.Handle);
            sut.Subscribe <TestMessage>("SubscriptionId 2",
                                        handler.Handle);

            // Assert
            string[] actual = sut.GetSubscriptionIdsForMessage <TestMessage>()
                                 .ToArray();

            Assert.True(actual.Contains("SubscriptionId 1"));
            Assert.True(actual.Contains("SubscriptionId 2"));
        }

        [Fact]
        public void Subscribe_AddsReplacesHandler_ForSameIdTwice()
        {
            // Arrange
            var handler = new TestActionHandler();
            var handlerOther = new TestActionHandlerOther();
            SubscriberRepository sut = CreateSut();

            // Act
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handlerOther.Handle);

            // Assert
            Assert.True(sut.Messages.Contains(typeof ( TestMessage )));
        }

        [Fact]
        public void Unsubscribe_RemovesIdFromIds_WhenCalled()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Act
            sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.False(actual.Contains("SubscriptionId"));
        }

        [Fact]
        public void Unsubscribe_DoesNothing_ForMessage()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Act
            sut.Unsubscribe <TestMessageOther>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(actual.Contains("SubscriptionId"));
        }

        [Fact]
        public void Unsubscribe_RemovesMessageFromMessages_ForNoIdsLeft()
        {
            // Arrange
            var handler = new TestActionHandler();
            SubscriberRepository sut = CreateSut();
            sut.Subscribe <TestMessage>("SubscriptionId",
                                        handler.Handle);

            // Act
            sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            Assert.False(sut.Messages.Contains(typeof ( TestMessage )));
        }

        private SubscriberRepository CreateSut()
        {
            return new SubscriberRepository();
        }

        private class TestActionHandler
        {
            public void Handle(TestMessage mesage)
            {
            }
        }

        private class TestActionHandlerOther
        {
            public void Handle(TestMessage mesage)
            {
            }
        }

        private class TestMessage
        {
        }

        public class TestMessageOther
        {
        }
    }
}