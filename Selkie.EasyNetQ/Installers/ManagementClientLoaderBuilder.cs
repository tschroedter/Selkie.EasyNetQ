using System.Diagnostics.CodeAnalysis;
using EasyNetQ.Management.Client;

namespace Selkie.EasyNetQ.Installers
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    public class ManagementClientLoaderBuilder
    {
        private const string HttpLocalhost = "http://localhost";
        private const string Username = "selkieAdmin";
        private const string Password = "selkieAdmin";

        public static ManagementClient CreateLoader()
        {
            var client = new ManagementClient(HttpLocalhost,
                                              Username,
                                              Password);
            return client;
        }
    }
}