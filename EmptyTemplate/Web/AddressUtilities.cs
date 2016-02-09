using System;
using System.Fabric;

namespace Web
{
    public static class AddressUtilities
    {
        public static string GetListeningAddress(string endpointName, string urlPrefix = null)
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentException(null, nameof(endpointName));
            }

            if (!string.IsNullOrEmpty(urlPrefix) && urlPrefix[0] != '/')
            {
                throw new ArgumentException(null, nameof(urlPrefix));
            }

            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(endpointName);

            string listeningAddress = $"{endpoint.Protocol}://+:{endpoint.Port}";

            if (!string.IsNullOrEmpty(urlPrefix))
            {
                listeningAddress += urlPrefix;
            }

            return listeningAddress;
        }

        public static string GetPublishingAddress(string listeningAddress)
        {
            return listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);
        }
    }
}
