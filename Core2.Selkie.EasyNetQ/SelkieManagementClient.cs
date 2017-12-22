using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Core2.EasyNetQ.Management.Client;
using Core2.EasyNetQ.Management.Client.Model;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.EasyNetQ
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class SelkieManagementClient : ISelkieManagementClient
    {
        public SelkieManagementClient([NotNull] ISelkieLogger logger,
                                      [NotNull] ManagementClient client,
                                      [NotNull] ICheckOrConfigureRabbitMq configure)
        {
            m_Logger = logger;
            m_Client = client;
            m_Configure = configure;
        }

        private const string VirtualHostName = "selkie";
        private readonly ManagementClient m_Client;
        private readonly ICheckOrConfigureRabbitMq m_Configure;
        private readonly ISelkieLogger m_Logger;

        public void DeleteAllBindings()
        {
            foreach ( Binding binding in m_Client.GetBindings() )
            {
                if ( VirtualHostName == binding.Vhost )
                {
                    m_Client.DeleteBinding(binding);
                }
            }
        }

        public void DeleteAllExchange()
        {
            foreach ( Exchange exchange in m_Client.GetExchanges() )
            {
                if ( VirtualHostName == exchange.Vhost )
                {
                    m_Client.DeleteExchange(exchange);
                }
            }
        }

        public void DeleteAllQueues()
        {
            foreach ( Queue queue in m_Client.GetQueues() )
            {
                if ( VirtualHostName == queue.Vhost )
                {
                    m_Client.DeleteQueue(queue);
                }
            }
        }

        public void DeleteAllQueues(string name)
        {
            string withContainingString = name.StartsWith("I")
                                              ? name.Substring(1)
                                              : name;

            foreach ( Queue queue in m_Client.GetQueues() )
            {
                if ( VirtualHostName == queue.Vhost &&
                     queue.Name.Contains(withContainingString) )
                {
                    m_Client.DeleteQueue(queue);
                }
            }
        }

        public void PurgeAllQueues()
        {
            foreach ( Queue queue in m_Client.GetQueues() )
            {
                if ( VirtualHostName == queue.Vhost )
                {
                    m_Client.Purge(queue);
                }
            }
        }

        public void PurgeAllQueues(string name)
        {
            string withContainingString = name.StartsWith("I")
                                              ? name.Substring(1)
                                              : name;

            foreach ( Queue queue in m_Client.GetQueues() )
            {
                if ( VirtualHostName != queue.Vhost ||
                     !queue.Name.Contains(withContainingString) )
                {
                    continue;
                }
                EmptyQueue(m_Client,
                           queue);
                m_Client.Purge(queue);
            }
        }

        public void PurgeQueueForServiceAndMessage(string name,
                                                   string messageName)
        {
            foreach ( Queue queue in m_Client.GetQueues() )
            {
                if ( VirtualHostName != queue.Vhost )
                {
                    continue;
                }
                string queueName = queue.Name;

                if ( queueName.Contains(name) &&
                     queueName.Contains(messageName) )
                {
                    m_Client.Purge(queue);
                }
            }
        }

        public void CheckOrConfigureRabbitMq()
        {
            m_Configure.CheckOrConfigure();
        }

        private void EmptyQueue([NotNull] IManagementClient client,
                                [NotNull] Queue queue)
        {
            try
            {
                var criteria = new GetMessagesCriteria(long.MaxValue,
                                                       false);
                IEnumerable <Message> messagesFromQueue = client.GetMessagesFromQueue(queue,
                                                                                      criteria);
                Message[] messages = messagesFromQueue.ToArray();

                foreach ( Message message in messages )
                {
                    m_Logger.Debug($"Removed {message.RoutingKey} message from queue {queue.Name}!");
                }

                m_Logger.Debug($"Removed {messages.Length} message from queue {queue.Name}!");
            }
            catch ( WebException wevException )
            {
                m_Logger.Error("Unknown error!",
                               wevException);
            }
        }
    }
}