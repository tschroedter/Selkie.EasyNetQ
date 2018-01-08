using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.EasyNetQ.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class SelkieEasyNetQLoggerTests
    {
        [Test]
        public void DebugWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.DebugWrite("Text");

            // Assert
            logger.Received().Debug("Text");
        }

        [Test]
        public void ErrorWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.ErrorWrite("Text");

            // Assert
            logger.Received().Error("Text");
        }

        [Test]
        public void ErrorWriteException_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);
            var exception = new ArgumentException("Text");

            // Act
            selkieLogger.ErrorWrite(exception);

            // Assert
            logger.Received().Error("An exception has occurred",
                                    exception);
        }

        [Test]
        public void InfoWrite_CallsDebug_WhenCalled()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var selkieLogger = new SelkieEasyNetQLogger(logger);

            // Act
            selkieLogger.InfoWrite("Text");

            // Assert
            logger.Received().Info("Text");
        }
    }
}