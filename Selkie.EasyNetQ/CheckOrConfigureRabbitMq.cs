using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Internal;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using JetBrains.Annotations;
using Selkie.EasyNetQ.Installers;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.EasyNetQ
{
    //ncrunch: no coverage start
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public class CheckOrConfigureRabbitMq : ICheckOrConfigureRabbitMq
    {
        private readonly ManagementClient m_Client;
        private readonly ISelkieLogger m_Logger;

        public CheckOrConfigureRabbitMq([NotNull] ISelkieLogger logger,
                                        [NotNull] ManagementClient client)
        {
            m_Logger = logger;
            m_Client = client;
        }

        public void CheckOrConfigure()
        {
            Vhost vhost = CheckOrCreateVirtualHost(BusBuilder.VirtualHost);

            CheckOrAddRabbitMqUser(ManagementClientLoaderBuilder.Username,
                                   ManagementClientLoaderBuilder.Password,
                                   "administrator",
                                   vhost);

            CheckOrAddRabbitMqUser(BusBuilder.Username,
                                   BusBuilder.Password,
                                   "",
                                   vhost);
        }

        private Vhost CheckOrCreateVirtualHost([NotNull] string virtualHost)
        {
            IEnumerable <Vhost> hosts = m_Client.GetVHosts();

            Vhost selkieHost = hosts.FirstOrDefault(x => x.Name == virtualHost);

            if ( selkieHost != null )
            {
                m_Logger.Info("Found RabbitMQ VirtualHost {0}!".Inject(virtualHost));
            }
            else
            {
                selkieHost = m_Client.CreateVirtualHost(virtualHost);

                m_Logger.Info("Created missing RabbitMQ VirtualHost {0}!".Inject(virtualHost));
            }

            return selkieHost;
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
                m_Logger.Info("Found RabbitMQ user {0}!".Inject(username));
            }
            else
            {
                CreateRabbitMqUser(username,
                                   password,
                                   tag,
                                   vhost);
            }
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

            m_Logger.Info("Created missing RabbitMQ user {0}!".Inject(username));

            var permissionInfo = new PermissionInfo(user,
                                                    vhost);
            permissionInfo.SetConfigure(".*");
            permissionInfo.SetRead(".*");
            permissionInfo.SetWrite(".*");

            m_Client.CreatePermission(permissionInfo);

            m_Logger.Info("Updated permissions for missing RabbitMQ user {0}!".Inject(username));
        }
    }
}