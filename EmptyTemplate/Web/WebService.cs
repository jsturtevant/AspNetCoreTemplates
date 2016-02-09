using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Collections.Generic;

namespace Web
{
    public class WebService : StatelessService
    {
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var webHost = new WebHostBuilder().UseDefaultConfiguration()
                                              .UseStartup<Startup>()
                                              .UseServer("Microsoft.AspNetCore.Server.WebListener")
                                              .UseUrls(WebListenerUtilities.GetListeningAddress("WebTypeEndpoint"))
                                              .Build();

            return new[] { new ServiceInstanceListener(_ => new AspNetCoreCommunicationListener(webHost)) };
        }
    }
}
