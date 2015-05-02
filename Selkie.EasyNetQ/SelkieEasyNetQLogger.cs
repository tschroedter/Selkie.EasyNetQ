﻿using System;
using Castle.Core.Logging;
using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.EasyNetQ
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieEasyNetQLogger : ISelkieEasyNetQLogger
    {
        private readonly ILogger m_Logger;

        public SelkieEasyNetQLogger([NotNull] ILogger logger)
        {
            m_Logger = logger;
        }

        public void DebugWrite([NotNull] string format,
                               [NotNull] params object[] args)
        {
            m_Logger.Debug(format);
        }

        public void InfoWrite([NotNull] string format,
                              [NotNull] params object[] args)
        {
            m_Logger.Info(format);
        }

        public void ErrorWrite([NotNull] string format,
                               [NotNull] params object[] args)
        {
            m_Logger.Error(format);
        }

        public void ErrorWrite([NotNull] Exception exception)
        {
            m_Logger.Error("An exception has occurred",
                           exception);
        }
    }
}