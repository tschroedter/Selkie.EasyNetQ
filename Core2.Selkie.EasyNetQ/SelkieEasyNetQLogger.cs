using System;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class SelkieEasyNetQLogger : ISelkieEasyNetQLogger
    {
        public SelkieEasyNetQLogger([NotNull] ISelkieLogger logger)
        {
            m_Logger = logger;
        }

        private readonly ISelkieLogger m_Logger;

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