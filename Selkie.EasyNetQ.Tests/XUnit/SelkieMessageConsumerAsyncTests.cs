﻿using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Selkie.EasyNetQ.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SelkieMessageConsumerAsyncTests
    {
        [Fact]
        public void ConsumeCallsHandle()
        {
            // Arrange
            SelkieMessageConsumerTests.Consumer sut = new SelkieMessageConsumerTests.Consumer();

            // Act
            sut.Handle(new SelkieMessageConsumerTests.TestMessage());

            // Assert
            Assert.True(sut.IsHandleCalled);
        }

        internal class Consumer : SelkieMessageConsumerAsync <SelkieMessageConsumerTests.TestMessage>
        {
            public bool IsHandleCalled;

            public override void Handle(SelkieMessageConsumerTests.TestMessage message)
            {
                IsHandleCalled = true;
            }
        }

        internal class TestMessage
        {
        }
    }
}