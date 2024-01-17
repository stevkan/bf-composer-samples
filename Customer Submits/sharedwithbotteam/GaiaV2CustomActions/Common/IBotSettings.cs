using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Interface for bot settings used by custom actions
    /// </summary>
    public interface IBotSettings
    {
        /// <summary>
        /// Get / Set CM Connection string
        /// </summary>
        string CmConnectionString { get; set; }

        /// <summary>
        /// Get / Set Gaia Authorized App ID for connection
        /// </summary>
        string GaiaAuthorizedAppId { get; set; }

        /// <summary>
        /// Get / Set Gaia subject name
        /// </summary>
        string GaiaApplicationCertificateSubjectName { get; set; }

        /// <summary>
        /// Get / Set Gaia Authorized Tenant
        /// </summary>
        string GaiaAuthorizedAppTenant { get; set; }

        /// <summary>
        /// Get Bot Name
        /// </summary>
        string ClusterAlias { get; set; }

        /// <summary>
        /// kuskus dm connection string
        /// </summary>
        public string KuskusDmConnectionString { get; set; }

        /// <summary>
        /// kuskus dm connection string
        /// </summary>
        public string KustoDomainName { get; set; }

        /// <summary>
        /// Gaia application certificate
        /// </summary>
        public X509Certificate2 GaiaCertificate { get; }
    }
}
