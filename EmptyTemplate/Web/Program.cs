using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Web
{
    public class Program
    {
        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseDefaultHostingConfiguration(args)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
