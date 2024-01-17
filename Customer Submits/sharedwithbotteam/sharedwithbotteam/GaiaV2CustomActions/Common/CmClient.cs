
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Class for actions on CM
    /// </summary>
    [Serializable]
    public class CmClient : ICmClient
    {
        

        
        [JsonProperty]
        public string CmConnectionString { get; set; }

        [JsonProperty]
        public Cloud CloudType { get; set; }

        
    }
}
