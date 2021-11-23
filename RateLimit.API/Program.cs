using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RateLimit.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var webhost = CreateHostBuilder(args).Build();


            //IP-Rate Ayarlari\\
            //var IpPolicy = webhost.Services.GetRequiredService<IIpPolicyStore>();

            /*GetService() ile GetServiceRequired arasindaki Fark
             *
             * GetService() kullanildiginda ilgili servis yoksa geriye null döner
             * GetRequiredService() kullanildiginda ilgili servis yoksa geriye hata firlatir.
             *
             * Eger bir servisin olmasi bekleniyorsa ve bu servis olmazsa uygulama patlayacaksa, mutlaka GetRequiredService() kullanilmalidir.
             */

            //IpPolicy.SeedAsync().Wait();

            //IP-Rate Ayarlari\\

            webhost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
