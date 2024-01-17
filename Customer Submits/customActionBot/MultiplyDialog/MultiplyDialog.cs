using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class MultiplyDialog : Dialog
{
    [JsonConstructor]
    public MultiplyDialog([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        : base()
    {
        // enable instances of this command as debug break point
        RegisterSourceLocation(sourceFilePath, sourceLineNumber);
        Properties.Add("myProp", DateTime.UtcNow.ToString());
        EventName = "myEvent";
    }

    [JsonProperty]
    IDictionary<string, string> Properties = new Dictionary<string, string>();

    [JsonProperty("$kind")]
    public const string Kind = "MultiplyDialog";

    [JsonProperty("arg1")]
    public NumberExpression Arg1 { get; set; }

    [JsonProperty("arg2")]
    public NumberExpression Arg2 { get; set; }

    [JsonProperty("resultProperty")]
    public StringExpression ResultProperty { get; set; }

    [JsonProperty]
    public String EventName { get; set; }

    public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
        TelemetryClient.TrackEvent(this.EventName, this.Properties);

        var arg1 = Arg1.GetValue(dc.State);
        var arg2 = Arg2.GetValue(dc.State);

        var result = Convert.ToInt32(arg1) * Convert.ToInt32(arg2);
        if (this.ResultProperty != null)
        {
            Thread.Sleep(3000);
            dc.State.SetValue(this.ResultProperty.GetValue(dc.State), result);
        }

        return dc.EndDialogAsync(result: result, cancellationToken: cancellationToken);
    }
}