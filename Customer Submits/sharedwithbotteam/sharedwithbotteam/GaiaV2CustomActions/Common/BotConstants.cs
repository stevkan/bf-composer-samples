
namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Class to hold all constants used by custom actions
    /// </summary>
    public class BotConstants
    {
        public const string BotSettingsPath = "botSettings";
        public const string SettingsPath = "settings." + BotSettingsPath;
        public const string BotName = "botberg";
        public const string KustoOpsMail = "KustoOps@microsoft.com";
        public const string AriaProxyConnection = "kusto.aria";
        public const string ClusterCloudType = "Please select on which Cloud your cluster is hosted?";
        public const int DeploymentsQueryNumberOfDays = 30;
        public const string CosmosDBId = "bot-db";
        public const string CosmosDBContainerId = "activities";

        #region Links and mail contacts
        public const string ServiceTreeLink = @"https://servicetree.msftcloudes.com/main.html#/ServiceModel/Service/Profile/{0}";
        public const string GenevaEngineDashboardUrl = c_genevaRootUrl + @"/MdmEngineMetrics/engine%2520health%2520V3?" + c_genevaQuery;
        public const string GenevaDmDashboardUrl = c_genevaRootUrl + @"/MdmDataMgmtMetrics/DM%2520health%2520v3?" + c_genevaQuery;
        #endregion

        private const string c_genevaRootUrl = @"https://jarvis-west.dc.ad.msft.net/dashboard/KustoProd";
        private const string c_genevaQuery = @"overrides=[{{%22query%22:%22//dataSources%22,%22key%22:%22account%22,%22replacement%22:%22{0}%22}},{{%22query%22:%22//*[id=%27Cluster%27]%22,%22key%22:%22value%22,%22replacement%22:%22{1}%22}},{{%22query%22:%22//*[id=%27Account%27]%22,%22key%22:%22value%22,%22replacement%22:%22%22}}]%20";
    }

    public enum Cloud
    {
        Public,
        Mooncake,
        BlackForest,
        Fairfax,
        USNat,
        USSec
    }
}
