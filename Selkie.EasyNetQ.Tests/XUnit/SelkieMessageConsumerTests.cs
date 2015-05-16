using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Selkie.EasyNetQ.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SelkieMessageConsumerTests
    {
        [Fact]
        public void ConsumeCallsHandle()
        {
            // Arrange
            var sut = new Consumer();

            // Act
            sut.Handle(new TestMessage());

            // Assert
            Assert.True(sut.IsHandleCalled);
        }

        internal class Consumer : SelkieMessageConsumer <TestMessage>
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
    }
}