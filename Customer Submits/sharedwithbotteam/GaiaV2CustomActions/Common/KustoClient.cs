using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Class for executing commands on Engine and DM
    /// </summary>
    [Serializable]
    public class KustoClient
    {
        

        [JsonProperty]
        public string SourceClusterPublicUrl { get; set; }

        [JsonProperty]
        public Cloud CloudType { get; set; }
        

        
    }
}
