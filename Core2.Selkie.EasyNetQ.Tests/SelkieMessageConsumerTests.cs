using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Core2.Selkie.EasyNetQ.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SelkieMessageConsumerTests
    {
        internal class Handler : SelkieMessageHandler <TestMessage>
        {
            public bool IsHandleCalled;

            public override void Handle(TestMessage message)
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
            sut.Handle(new TestMessage());

            // Assert
            Assert.True(sut.IsHandleCalled);
        }
    }
}