using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Declarative.Resources;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SkyApollo.Core.CustomActions.Utilities
{
    public class DynamicDialogLoading : Dialog
    {
        [JsonProperty("$kind")]
        public const string Kind = "DynamicDialogLoading";

        [JsonConstructor]
        public DynamicDialogLoading([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        }

        [JsonProperty("dialog")]
        public DialogExpression Dialog { get; set; }

        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            ResolveDialog(dc);
            return dc.EndDialogAsync(null, cancellationToken);
        }

        /// <summary>
        /// See <see cref="Microsoft.Bot.Builder.Dialogs.Adaptive.Actions.BeginDialog"/>
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        protected Dialog ResolveDialog(DialogContext dc)
        {
            if (Dialog == null)
            {
                return null;
            }

            if (Dialog.Value != null)
            {
                return Dialog.Value;
            }

            // NOTE: we want the result of the expression as a string so we can look up the string using external FindDialog().
            var se = new StringExpression($"={Dialog.ExpressionText}");
            var dialogId = se.GetValue(dc.State);

            if (string.IsNullOrEmpty(dialogId))
            {
                return null;
            }

            var dialog = dc.FindDialog(dialogId);

            if (dialog != null)
            {
                return dialog;
            }

            var resourceExplorer = dc.Context.TurnState.Get<ResourceExplorer>();
            dialog = resourceExplorer.LoadType<AdaptiveDialog>($"{dialogId}.dialog");

            if (dialog != null)
            {
                dc.Dialogs.Add(dialog);
                return dialog;
            }

            throw new NullReferenceException($"Dialog {dialogId} not found");
        }
    }
}