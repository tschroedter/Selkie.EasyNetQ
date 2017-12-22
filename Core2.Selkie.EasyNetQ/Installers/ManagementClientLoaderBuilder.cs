using System.Diagnostics.CodeAnalysis;
using Core2.EasyNetQ.Management.Client;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class ManagementClientLoaderBuilder
    {
        private const string HttpLocalhost = "http://localhost";    // Todo this is private the other 2 not? maybe new class
        internal const string Username = "selkieAdmin";
        internal const string Password = "selkieAdmin";

        public static ManagementClient CreateLoader()
        {
            var client = new ManagementClient(HttpLocalhost,
                                              Username,
                                              Password);
            return client;
        }
    }
}