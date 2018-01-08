using System.Diagnostics.CodeAnalysis;
using Core2.EasyNetQ.Management.Client;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ.Installers
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class ManagementClientLoaderBuilder
    {
        public static ManagementClient CreateLoader()
        {
            var client = new ManagementClient(ManagementClientConfigurationProvider.HttpLocalhost,
                                              ManagementClientConfigurationProvider.Username,
                                              ManagementClientConfigurationProvider.Password);
            return client;
        }
    }
}