using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Internal;
using Core2.EasyNetQ.Management.Client;
using Core2.EasyNetQ.Management.Client.Model;
using Core2.Selkie.EasyNetQ.Installers;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class CheckOrConfigureRabbitMq : ICheckOrConfigureRabbitMq
    {
        public CheckOrConfigureRabbitMq([NotNull] ISelkieLogger logger,
                                        [NotNull] ManagementClient client)
        {
            m_Logger = logger;
            m_Client = client;
        }

        private readonly ManagementClient m_Client;
        private readonly ISelkieLogger m_Logger;

        public void CheckOrConfigure()
        {
            Vhost vhost = CheckOrCreateVirtualHost(BusBuilder.VirtualHost);

            CheckOrAddRabbitMqUser(ManagementClientConfigurationProvider.Username,
                                   ManagementClientConfigurationProvider.Password,
                                   "administrator",
                                   vhost);

            CheckOrAddRabbitMqUser(BusBuilder.Username,
                                   BusBuilder.Password,
                                   "",
                                   vhost);
        }

        private void CheckOrAddRabbitMqUser([NotNull] string username,
                                            [NotNull] string password,
                                            [NotNull] string tag,
                                            [NotNull] Vhost vhost)
        {
            IEnumerable <User> users = m_Client.GetUsers();

            User user = users.FirstOrDefault(x => x.Name == username);

            if ( user != null )
            {
                m_Logger.Info($"Found RabbitMQ user {username}!");
            }
            else
            {
                CreateRabbitMqUser(username,
                                   password,
                                   tag,
                                   vhost);
            }
        }

        private Vhost CheckOrCreateVirtualHost([NotNull] string virtualHost)
        {
            IEnumerable <Vhost> hosts = m_Client.GetVHosts();

            Vhost selkieHost = hosts.FirstOrDefault(x => x.Name == virtualHost);

            if ( selkieHost != null )
            {
                m_Logger.Info($"Found RabbitMQ VirtualHost {virtualHost}!");
            }
            else
            {
                selkieHost = m_Client.CreateVirtualHost(virtualHost);

                m_Logger.Info($"Created missing RabbitMQ VirtualHost {virtualHost}!");
            }

            return selkieHost;
        }

        private void CreateRabbitMqUser([NotNull] string username,
                                        [NotNull] string password,
                                        [NotNull] string tag,
                                        [NotNull] Vhost vhost)
        {
            var userInfo = new UserInfo(username,
                                        password);

            if ( !tag.IsNullOrEmpty() )
            {
                userInfo.AddTag(tag);
            }

            User user = m_Client.CreateUser(userInfo);

            m_Logger.Info($"Created missing RabbitMQ user {username}!");

            var permissionInfo = new PermissionInfo(user,
                                                    vhost);
            permissionInfo.SetConfigure(".*");
            permissionInfo.SetRead(".*");
            permissionInfo.SetWrite(".*");

            m_Client.CreatePermission(permissionInfo);

            m_Logger.Info($"Updated permissions for missing RabbitMQ user {username}!");
        }
    }
}