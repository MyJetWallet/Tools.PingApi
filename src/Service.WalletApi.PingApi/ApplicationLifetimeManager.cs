using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.WalletApi.Wallets;
using MyNoSqlServer.DataReader;

namespace Service.WalletApi.PingApi
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly MyNoSqlTcpClient _myNoSqlTcpClient;

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime, ILogger<ApplicationLifetimeManager> logger,
            IWalletService walletService,
            MyNoSqlTcpClient myNoSqlTcpClient)
            : base(appLifetime)
        {
            _logger = logger;
            _myNoSqlTcpClient = myNoSqlTcpClient;

            // init static locator to implement helper - Get wallet
            MyJetWallet.Sdk.WalletApi.ControllerUtils.WalletService = walletService;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            //_myNoSqlTcpClient.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            try
            {
                //_myNoSqlTcpClient.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot stop nosql\n{e}");
            }
        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
