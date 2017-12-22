using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Core2.Selkie.EasyNetQ.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SelkieMessageConsumerAsyncTests
    {
        internal class Handler : SelkieMessageHandlerAsync <SelkieMessageConsumerTests.TestMessage>
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

        [Test]
        public void ConsumeCallsHandle()
        {
            // Arrange
            var sut = new Handler();

            // Act
            sut.Handle(new SelkieMessageConsumerTests.TestMessage());

            // Assert
            Assert.True(sut.IsHandleCalled);
        }
    }
}