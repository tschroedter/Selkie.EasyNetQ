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
        public void DebugWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.DebugWrite("Text");

            // Assert
            logger.Received().Debug("Text");
        }

        [Fact]
        public void InfoWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.InfoWrite("Text");

            // Assert
            logger.Received().Info("Text");
        }

        [Fact]
        public void ErrorWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.ErrorWrite("Text");

            // Assert
            logger.Received().Error("Text");
        }

        [Fact]
        public void ErrorWriteException_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ILogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);
            var exception = new ArgumentException("Text");

            // Act
            selkieLogger.ErrorWrite(exception);

            // Assert
            logger.Received().Error("An exception has occurred",
                                    exception);
        }
    }
}