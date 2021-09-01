using Autofac;
using MyJetWallet.Sdk.Authorization.NoSql;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.Service;
using MyNoSqlServer.DataReader;
using Service.AssetsDictionary.Client;
using Service.ClientWallets.Client;

namespace Service.WalletApi.PingApi.Modules
{
    public class ClientsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var myNoSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));

            builder.RegisterAssetsDictionaryClients(myNoSqlClient);

            builder.RegisterClientWalletsClients(myNoSqlClient, Program.Settings.ClientWalletsGrpcServiceUrl);

            RegisterAuthServices(builder);
        }

        protected void RegisterAuthServices(ContainerBuilder builder)
        {
            // he we do not use CreateNoSqlClient beacuse we have a problem with start many mynosql instances 
            var authNoSql = new MyNoSqlTcpClient(
                Program.ReloadedSettings(e => e.AuthMyNoSqlReaderHostPort),
                ApplicationEnvironment.HostName ?? $"{ApplicationEnvironment.AppName}:{ApplicationEnvironment.AppVersion}");

            builder.RegisterMyNoSqlReader<ShortRootSessionNoSqlEntity>(authNoSql, ShortRootSessionNoSqlEntity.TableName);

            //authNoSql.Start();
        }
    }
}