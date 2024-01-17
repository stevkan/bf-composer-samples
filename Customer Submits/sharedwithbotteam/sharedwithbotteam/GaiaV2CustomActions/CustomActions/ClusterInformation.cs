using AdaptiveExpressions.Properties;
using GaiaV2CustomActions.Common;
using GaiaV2CustomActions.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GaiaV2CustomActions.CustomActions
{
    /// <summary>
    /// Custom action for basic Cluster Information  TBD - may need to change to use new API service once available 
    /// Refer https://docs.microsoft.com/en-us/composer/how-to-create-custom-actions 
    /// </summary>
    public class ClusterInformation : Dialog
    {
        private IClients m_clients;
        private ICmClient m_cmClient;

        [JsonConstructor]
        public ClusterInformation([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        : base()
        {
            // enable instances of this command as debug break point
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// Kind of this action used by bot composer
        /// </summary>
        [JsonProperty("$kind")]
        public const string Kind = nameof(ClusterInformation);

        /// <summary>
        /// Cluster name to be used
        /// </summary>
        [JsonProperty("clusterName")]
        public StringExpression ClusterName { get; set; }

        /// <summary>
        /// Result property to be used in bot
        /// </summary>
        [JsonProperty("resultProperty")]
        public StringExpression ResultProperty {get; set;}

        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dialogContext, object options = null, CancellationToken cancellationToken = default)
        {
            var clusterName = ClusterName.GetValue(dialogContext.State);
            m_clients = dialogContext.Context.TurnState.Get<IClients>();
            return dialogContext.EndDialogAsync(result: null, cancellationToken: cancellationToken);
        }

        
    }
}
