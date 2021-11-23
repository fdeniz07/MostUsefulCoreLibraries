using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace RateLimit.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //RATE LIMIT AYARLARI\\
            services.AddOptions();
            services.AddMemoryCache(); //Dakika yapilan request sayilarini ram'de tutar

            //IP-RATE LIMIT
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting")); //appsettings.json icerisinde verecegimiz baglanti limitleri
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies")); //appsettings.json icerisinde verecegimiz kurallar

            //services.AddSingleton<IIpPolicyStore, MemoryCacheClientPolicyStore>(); //Eski sürümlerde gecerli - Uygulamamizda tek instance ayaga kalkiyorsa MemoryCacheClientPolicyStore kullanilir. Birden fazla instance ayaga kalkiyorsa (örnek: Docker da 5 container ayaga kaldirirsak, merkezi bir yerde sayac tutulmasi gerekir. Bunun icinde DistributedCacheClientPolicyStore kullanmamiz gerekir. REDIS gibi.)

            //CLIENT RATE LIMIT
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting")); //appsettings.json icerisinde verecegimiz baglanti limitleri
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies")); //appsettings.json icerisinde verecegimiz kurallar
            services.AddInMemoryRateLimiting(); //Yeni sürüm icin gecerli

            //services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>(); //Eski sürümlerde gecerli

            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>(); //Eski sürümlerde gecerli - yukaridaki ile ayni

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Defaultta kapali gelen bir servis. Ihtiyac varsa bu özellik acilir.Middleware katmanlar icin olusturulur. Gelen Request icerisindeki header bilgisi, IP adresi gibi bilgileri ayni zamanda http metot tipi (get,put,post) bunlari ayri ayri okumaya yarar.

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            /*Middleware Nedir
             *
             * Güvenlik katmanlarinin yani sira, gerekli ise yeniden ele alinmasi, farkli veri kaynaklarindan gelen verilerin birlestirilmesi, bir veya daha fazla veri kaynagina iletilmesi, ayristirilmasi gibi pek cok tekrarlanan islemler söz konusu olabilir.
             *
             * Middleware (ara yazilim) tekrar eden islemleri üstlenen, amaca yönelik görevleri gerceklestiren cözümlerden biridir.
             */
            //RATE LIMIT AYARLARI\\

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RateLimit.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RateLimit.API v1"));
            }

            //Ip-Rate Limit
            //app.UseIpRateLimiting(); // IpRateLimit middleware yukarida tanimlanir,buraya eklenir. 

            //Client-Rate Limit
            app.UseClientRateLimiting(); // ClientRateLimit middleware yukarida tanimlanir,buraya eklenir. 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
