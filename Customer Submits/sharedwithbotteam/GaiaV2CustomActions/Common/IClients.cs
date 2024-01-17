
using System;

namespace GaiaV2CustomActions.Common
{
    /// <summary>
    /// Class to store clients used by custom actions
    /// </summary>
    public interface IClients
    {
        /// <summary>
        /// Gets the cm client
        /// </summary>
        ICmClient CmClient { get; }

        /// <summary>
        /// Gets the engine client
        /// </summary>
        IEngineClient EngineClient { get; }

        /// <summary>
        /// Gets the engine client
        /// </summary>
        IDmClient DmClient { get; }

        /// <summary>
        /// Initialize clients
        /// </summary>
        /// <param name="botSettings"></param>
        void Initialize(IBotSettings botSettings, string clientActivityId);
    }    
}
