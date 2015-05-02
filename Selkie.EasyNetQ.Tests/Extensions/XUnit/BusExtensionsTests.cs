using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.EasyNetQ.Extensions;
using Xunit;

namespace Selkie.EasyNetQ.Tests.Extensions.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class BusExtensionsTests
    {
        private bool m_TestHandlerWasCalled;

        private void TestHandler([NotNull] TestMessage message)
        {
            m_TestHandlerWasCalled = true;
        }

        [Fact]
        public void CreateTaskCallsLoggerTest()
        {
            ILogger logger = Substitute.For <ILogger>();

            BusExtensions.CreateTask(logger,
                                     TestHandler,
                                     new TestMessage());

            logger.Received()
                  .Debug(Arg.Is <string>(x => x.StartsWith("Received ")));
        }

        [Fact]
        public void CreateTaskReturnsTaskTest()
        {
            ILogger logger = Substitute.For <ILogger>();

            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage());

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreatedTaskCallsHandlerTest()
        {
            ILogger logger = Substitute.For <ILogger>();

            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage());

            actual.Wait();

            Assert.True(m_TestHandlerWasCalled);
        }

        [Fact]
        public void CreatedTaskCallsLoggerTest()
        {
            ILogger logger = Substitute.For <ILogger>();

            Task actual = BusExtensions.CreateTask(logger,
                                                   TestHandler,
                                                   new TestMessage());

            actual.Wait();

            logger.Received()
                  .Debug(Arg.Is <string>(x => x.StartsWith("Handling ")));
        }

        [Fact]
        public void SubscribeHandlerCallsBusTest()
        {
            IBus bus = Substitute.For <IBus>();
            ILogger logger = Substitute.For <ILogger>();

            bus.SubscribeHandlerAsync <TestMessage>(logger,
                                                    "TestId",
                                                    TestHandler);

            bus.Received()
               .SubscribeAsync("TestId",
                               Arg.Any <Func <TestMessage, Task>>());
        }

        [UsedImplicitly]
        private class TestMessage
        {
        }
    }
}