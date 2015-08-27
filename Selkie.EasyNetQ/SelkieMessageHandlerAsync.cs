using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using JetBrains.Annotations;

namespace Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public abstract class SelkieMessageHandlerAsync <T> : IConsumeAsync <T>
        where T : class
    {
        public Task Consume([NotNull] T message)
        {
            Task task = Task.Factory.StartNew(() =>
                                              {
                                                  Handle(message);
                                              });

            return task;
        }

        public abstract void Handle(T message);
    }
}