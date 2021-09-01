using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.WalletApi.PingApi.Settings
{
    public class SettingsModel
    {
        [YamlProperty("WalletApi.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("WalletApi.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("WalletApi.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("WalletApi.EnableApiTrace")]
        public bool EnableApiTrace { get; set; }

        [YamlProperty("WalletApi.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("WalletApi.AuthMyNoSqlReaderHostPort")]
        public string AuthMyNoSqlReaderHostPort { get; set; }

        [YamlProperty("WalletApi.ClientWalletsGrpcServiceUrl")]
        public string ClientWalletsGrpcServiceUrl { get; set; }


    }
}