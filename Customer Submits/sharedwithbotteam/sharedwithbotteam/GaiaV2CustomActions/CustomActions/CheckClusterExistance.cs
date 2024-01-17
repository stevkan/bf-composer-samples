using AdaptiveExpressions.Properties;
using GaiaV2CustomActions.Common;

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace GaiaV2CustomActions.CustomActions
{
    public class CheckClusterExistance : Dialog
    {
        private IClients m_clients;
        private string m_clusterName;
        private Cloud? m_cloud;

        [JsonConstructor]
        public CheckClusterExistance([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        : base()
        {
            // enable instances of this command as debug break point
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }

        /// <summary>
        /// Kind of this action used by bot composer
        /// </summary>
        [JsonProperty("$kind")]
        public const string Kind = nameof(CheckClusterExistance);

        /// <summary>
        /// Cluster name to be used
        /// </summary>
        [JsonProperty("clusterName")]
        public StringExpression ClusterName { get; set; }

        /// <summary>
        /// Result property to be used in bot
        /// </summary>
        [JsonProperty("resultProperty")]
        public StringExpression ResultProperty { get; set; }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dialogContext, object options = null, CancellationToken cancellationToken = default)
        {
            m_clients = dialogContext.Context.TurnState.Get<IClients>();
            var clusterNameFromBot = ClusterName.GetValue(dialogContext.State);
            var clusterName = ClusterName.GetValue(dialogContext.State);
            m_clients = dialogContext.Context.TurnState.Get<IClients>();
            return await dialogContext.EndDialogAsync(result: null, cancellationToken: cancellationToken);
        }

        
    }
}
