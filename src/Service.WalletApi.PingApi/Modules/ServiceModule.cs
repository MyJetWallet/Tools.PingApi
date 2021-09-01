using Autofac;
using MyJetWallet.Sdk.RestApiTrace;
using MyJetWallet.Sdk.WalletApi.Wallets;

namespace Service.WalletApi.PingApi.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<WalletService>()
                .As<IWalletService>()
                .SingleInstance();

            if (Program.Settings.EnableApiTrace)
            {
                builder
                    .RegisterInstance(new ApiTraceManager(Program.Settings.ElkLogs, "api-trace",
                        Program.LoggerFactory.CreateLogger("ApiTraceManager")))
                    .As<IApiTraceManager>()
                    .As<IStartable>()
                    .AutoActivate()
                    .SingleInstance();
            }
        }
    }
}