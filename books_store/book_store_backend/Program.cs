using books;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Internal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args) 
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    // clear default logging providers
                    logging.ClearProviders();

                    // add built-in providers manually, as needed 
                    logging.AddConsole();
                    logging.AddDebug(); 
                    logging.AddEventSourceLogger(); 
                });
    }
}
