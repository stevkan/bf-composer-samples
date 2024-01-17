
using Newtonsoft.Json;
using System;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Class to store clients used by custom actions
    /// </summary>
    [Serializable]
    public class Clients : IClients
    {
        #region Private members
        private Lazy<ICmClient> m_cmClient;
        private Lazy<IEngineClient> m_engineClient;
        private Lazy<IDmClient> m_dmClient;
        #endregion

        #region Constructor
        public Clients() 
        {
            m_cmClient = new Lazy<ICmClient>(() =>
            {
                return new CmClient();
            });

            m_dmClient = new Lazy<IDmClient>(() =>
            {
                return new DmClient();
            });

            m_engineClient = new Lazy<IEngineClient>(() =>
            {
                return new EngineClient();
            });
        }
        #endregion

        #region Public members
        [JsonProperty]
        public ICmClient CmClient => m_cmClient.Value;

        [JsonProperty]
        public IEngineClient EngineClient => m_engineClient.Value;

        [JsonProperty]
        public IDmClient DmClient => m_dmClient.Value;
        
        public void Initialize(IBotSettings botSettings, string clientActivityId)
        {
            //CmClient.Initialize(botSettings, clientActivityId);
            //EngineClient.Initialize(botSettings, clientActivityId);
            //DmClient.Initialize(botSettings, clientActivityId);
            
        }
        #endregion

        
    }
}
