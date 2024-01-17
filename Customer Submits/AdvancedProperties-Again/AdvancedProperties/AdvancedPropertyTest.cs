using System;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveExpressions.Properties;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;


[Serializable]
public class SkillSelector
{

    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [JsonProperty(PropertyName = "operator")]
    public string Operator { get; set; }

    [JsonProperty(PropertyName = "value")]
    public int Value { get; set; }
}


public class AdvancedPropertyTest : Dialog
{
    [JsonProperty("skillSelectors")]
    public ArrayExpression<SkillSelector> SkillSelectors { get; set; }
    //public ArrayExpression<Metadata> SkillSelectors { get; set; }


    public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
    {
        var skillSelectors = SkillSelectors?.GetValue(dc.State);
        // some code

        return await dc.EndDialogAsync(cancellationToken: cancellationToken);
    }
}
