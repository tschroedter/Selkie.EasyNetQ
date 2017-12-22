using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.EasyNetQ.Extensions;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.EasyNetQ.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class BusExtensionsTests
    {
        private bool m_TestHandlerWasCalled;

        private void TestHandler([NotNull] TestMessage message)
        {
            m_TestHandlerWasCalled = true;
        }

        [UsedImplicitly]
        private class TestMessage
        {
        }

        [Test]
        public void CreatedTask_CallsHandler_WhenExecuted()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var padlock = new object();

            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage(),
                                                   padlock);

            // Act
            actual.Wait();

            // Assert
            Assert.True(m_TestHandlerWasCalled);
        }

        [Test]
        public void CreatedTask_CallsLogger_WhenExecuted()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var padlock = new object();

            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage(),
                                                   padlock);

            // Act
            actual.Wait();

            // Assert
            logger.Received().Debug(Arg.Is <string>(x => x.StartsWith("Handling ")));
        }

        [Test]
        public void CreateTask_CallsLogger_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var padlock = new object();

            // Act
            BusExtensions.CreateTask(logger,
                                     TestHandler,
                                     new TestMessage(),
                                     padlock);

            // Assert
            logger.Received().Debug(Arg.Is <string>(x => x.StartsWith("Received ")));
        }

        [Test]
        public void CreateTask_ReturnsTask_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var padlock = new object();

            // Act
            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage(),
                                                   padlock);

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void FindOrCreatePadlock_CreatesPadlock_ForNewSubscriptionId()
        {
            // Arrange
            BusExtensions.FindOrCreatePadlock("subscriptionId");

            // Act
            object actual = BusExtensions.Padlocks [ "subscriptionId" ];

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void FindOrCreatePadlock_ReturnsExistingPadlock_ForKnownSubscriptionId()
        {
            // Arrange
            BusExtensions.FindOrCreatePadlock("subscriptionId");
            object expected = BusExtensions.Padlocks [ "subscriptionId" ];

            BusExtensions.FindOrCreatePadlock("subscriptionId");

            // Act
            object actual = BusExtensions.Padlocks [ "subscriptionId" ];

            // Assert
            Assert.True(expected == actual);
        }

        [Test]
        public void SubscribeHandlerAsync_CallsBus_WhenCalled()
        {
            // Arrange
            var bus = Substitute.For <IBus>();
            var logger = Substitute.For <ISelkieLogger>();

            // Act
            bus.SubscribeHandlerAsync <TestMessage>(logger,
                                                    "TestId",
                                                    TestHandler);

            // Assert
            bus.Received().SubscribeAsync("TestId",
                                          Arg.Any <Func <TestMessage, Task>>());
        }
    }
}