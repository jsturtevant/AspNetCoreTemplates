using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using System.Fabric;
using System.IO;

namespace Web
{
    public class AspnetCoreCommunicationListener : ICommunicationListener
    {
        private StatelessServiceContext serviceContext;
        private string endpointName;
        private IWebHostBuilder webHostBuilder;

        private IWebHost webHost;

        public AspnetCoreCommunicationListener(IWebHostBuilder webhostBuilder, StatelessServiceContext serviceContext, string endpointName)
        {
            this.webHostBuilder = webhostBuilder;
            this.serviceContext = serviceContext;
            this.endpointName = endpointName;
        }


        public void Abort()
        {
            if (webHost != null)
            {
                webHost.Dispose();
            }

        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            if (webHost != null)
            {
                webHost.Dispose();
            }

            return Task.FromResult(true);
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(endpointName);

            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}";
        

            webHost = webHostBuilder.UseKestrel()
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseUrls(serverUrl)
                                    .Build();

            webHost.Start();


            return Task.FromResult(serverUrl);
        }
    }
}
