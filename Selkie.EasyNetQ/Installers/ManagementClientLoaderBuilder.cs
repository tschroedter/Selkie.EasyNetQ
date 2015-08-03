using System.Diagnostics.CodeAnalysis;
using EasyNetQ.Management.Client;

namespace Selkie.EasyNetQ.Installers
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class ManagementClientLoaderBuilder
    {
        internal const string HttpLocalhost = "http://localhost";
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