using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// class for bot settings used by custom actions
    /// </summary>
    public class BotSettings : IBotSettings
    {
        #region AppSettings Keys
        private const string c_gaiaAuthorizedAppId = "GaiaAuthorizedAppId";
        private const string c_gaiaAuthorizedAppTenant = "GaiaAuthorizedAppTenant";
        private const string c_clusterAlias = "ClusterAlias";
        private const string c_gaiaApplicationSubjectName = "GaiaApplicationCertificateSubjectName";
        private const string c_kuskusDmConnectionString = "KuskusDmConnectionString";
        private const string c_cosmosDbEndpoint = "CosmosDbEndpoint";
        private const string c_cosmosDbAuthKey = "CosmosDbAuthKey";
        private const string c_cmConnectionString = "CmConnectionString";
        private const string c_encryptionCertThumbprint = "EncryptionCertThumbprint";
        private const string c_kustoDomainName = "KustoDomainName";
        #endregion

        public string CmConnectionString { get; set; }
        public string GaiaAuthorizedAppId { get; set; }
        public string GaiaApplicationCertificateSubjectName { get; set; }
        public string GaiaAuthorizedAppTenant { get; set; }
        public string ClusterAlias { get; set; }
        public string KuskusDmConnectionString { get; set; }
        public string CosmosDbEndpoint { get; set; }
        public string CosmosDbAuthKey { get; set; }
        public string KustoDomainName { get; set; }

        public X509Certificate2 GaiaCertificate { get; private set; }

        #region Constructor
        public BotSettings() { }

        public BotSettings(IConfiguration configuration)
        {
            //var configurationDecryptUtil = new ConfigurationDecryptUtil();
            var botSettings = configuration.GetSection(BotConstants.BotSettingsPath);
            var encryptionCertThumbprint = botSettings?.GetSection(c_encryptionCertThumbprint).Value;
            GaiaApplicationCertificateSubjectName = botSettings?.GetSection(c_gaiaApplicationSubjectName).Value;
            GaiaAuthorizedAppId = botSettings?.GetSection(c_gaiaAuthorizedAppId).Value;
            GaiaAuthorizedAppTenant = botSettings?.GetSection(c_gaiaAuthorizedAppTenant).Value;
            ClusterAlias = botSettings?.GetSection(c_clusterAlias).Value;
            KuskusDmConnectionString = botSettings?.GetSection(c_kuskusDmConnectionString).Value;
            CmConnectionString = botSettings?.GetSection(c_cmConnectionString).Value;
            CosmosDbEndpoint = botSettings?.GetSection(c_cosmosDbEndpoint).Value;
            CosmosDbAuthKey = botSettings?.GetSection(c_cosmosDbAuthKey).Value; //configurationDecryptUtil.DecryptConfigurationValue(botSettings?.GetSection(c_cosmosDbAuthKey).Value, encryptionCertThumbprint);
            KustoDomainName = botSettings?.GetSection(c_kustoDomainName).Value;
            //GaiaCertificate = CertificateUtilities.TryLoadPersonalLatestCertificateBySubjectDistinguishedName(GaiaApplicationCertificateSubjectName);
        }
        #endregion // Constructor
    }
}
