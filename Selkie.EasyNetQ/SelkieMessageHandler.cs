using System.Diagnostics.CodeAnalysis;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    public abstract class SelkieMessageHandler <T> : IConsume <T>
        where T : class
    {
        public void Consume([NotNull] T message)
        {
            Handle(message);
        }

        public abstract void Handle(T message);
    }
}