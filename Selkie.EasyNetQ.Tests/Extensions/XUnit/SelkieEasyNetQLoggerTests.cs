using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using NSubstitute;
using Xunit;

namespace Selkie.EasyNetQ.Tests.Extensions.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class SelkieEasyNetQLoggerTests
    {
        [Fact]
        public void DebugWriteCallsDebugTest()
        {
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            selkieLogger.DebugWrite("Text");

            logger.Received().Debug("Text");
        }

        [Fact]
        public void InfoWriteCallsDebugTest()
        {
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            selkieLogger.InfoWrite("Text");

            logger.Received().Info("Text");
        }

        [Fact]
        public void ErrorWriteCallsDebugTest()
        {
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            selkieLogger.ErrorWrite("Text");

            logger.Received().Error("Text");
        }

        [Fact]
        public void ErrorWriteExceptionCallsDebugTest()
        {
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);
            var exception = new ArgumentException("Text");

            selkieLogger.ErrorWrite(exception);

            logger.Received().Error("An exception has occurred",
                                    exception);
        }
    }
}