using DaminLibrary.Expansion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class ServiceHostAdvance<T>
    {
        private readonly string host;
        private readonly decimal port;
        private ServiceHost serviceHost;
        public ServiceHostAdvance(string host, decimal port)
        {
            this.host = host;
            this.port = port;
        }
        public void Open()
        {
            Type type = typeof(T);
            this.serviceHost = new ServiceHost(type);
            Type getInterfaceType = type.GetInterfaceType();
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
            string createClassName = getInterfaceType.CreateClassName();
            string address = "net.tcp://" + this.host + ":" + this.port + "/" + createClassName;
            ServiceEndpoint addServiceEndpoint = this.serviceHost.AddServiceEndpoint(getInterfaceType, netTcpBinding, address);
            this.serviceHost.Open();
        }
        public void Close()
        {
            this.serviceHost.Close();
        }
    }
}
