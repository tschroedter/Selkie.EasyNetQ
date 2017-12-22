using System.Diagnostics.CodeAnalysis;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public abstract class SelkieMessageHandler <T> : IConsume <T>
        where T : class
    {
        public void Consume([NotNull] T message)
        {
            Handle(message);
        }

        [UsedImplicitly]
        public abstract void Handle(T message);
    }
}