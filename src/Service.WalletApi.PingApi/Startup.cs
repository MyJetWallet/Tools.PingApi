using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.WalletApi;
using Service.WalletApi.PingApi.Modules;

namespace Service.WalletApi.PingApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            StartupUtils.SetupWalletServices(services);
            
            services.AddHostedService<ApplicationLifetimeManager>();

            services
                .AddSignalR(option =>
                {
                    option.EnableDetailedErrors = true;

                })
                //.AddMessagePackProtocol()
                ;

            services.AddMyTelemetry("SP-", Program.Settings.ZipkinUrl);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StartupUtils.SetupWalletApplication(app, env, Program.Settings.EnableApiTrace, "ping");

            app.UseWebSockets();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseFileServer();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new ClientsModule());
            builder.RegisterModule(new ServiceBusModule());
        }
    }
}
