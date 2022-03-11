using DaminLibrary.Expansion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class ChannelFactoryAdvance<T>
    {
        private readonly string host;
        private readonly int port;
        private ChannelFactory<T> channelFactory;
        public T CreateChannel()
        {
            this.channelFactory = new ChannelFactory<T>();
            Type type = typeof(T);
            string createClassName = type.CreateClassName();
            string address = "net.tcp://" + this.host + ":" + this.port + "/" + createClassName;
            var endpointAddress = new EndpointAddress(address);
            this.channelFactory.Endpoint.Address = endpointAddress;
            var netTcpBinding = new NetTcpBinding();
            netTcpBinding.Security = new NetTcpSecurity();
            netTcpBinding.Security.Mode = SecurityMode.None;
            netTcpBinding.MaxConnections = 10000;
            netTcpBinding.MaxReceivedMessageSize = 2147483647;
            netTcpBinding.MaxBufferSize = 2147483647;
            netTcpBinding.MaxBufferPoolSize = 2147483647;
            var timeSpan = new TimeSpan(1, 0, 0);
            netTcpBinding.OpenTimeout = timeSpan;
            netTcpBinding.CloseTimeout = timeSpan;
            netTcpBinding.SendTimeout = timeSpan;
            netTcpBinding.ReceiveTimeout = timeSpan;
            this.channelFactory.Endpoint.Binding = netTcpBinding;
            this.channelFactory.Endpoint.Contract.ContractType = typeof(T);
            T createChannel = this.channelFactory.CreateChannel();
            return createChannel;
        }
        public ChannelFactoryAdvance(string host, int port)
        {
            this.host = host;
            this.port = port;
        }
    }
}
