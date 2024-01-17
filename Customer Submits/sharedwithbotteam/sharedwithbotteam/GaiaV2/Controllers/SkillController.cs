using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace GaiaV2.Controllers
{
    /// <summary>
    /// A controller that handles skill replies to the bot.
    /// </summary>
    [ApiController]
    [Route("api/skills")]
    public class SkillController : ChannelServiceController
    {
        public SkillController(ChannelServiceHandlerBase handler)
            : base(handler)
        {
        }

        public override Task<IActionResult> ReplyToActivityAsync(string conversationId, string activityId, Activity activity)
        {
            try
            {

                return base.ReplyToActivityAsync(conversationId, activityId, activity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Task<IActionResult> SendToConversationAsync(string conversationId, Activity activity)
        {
            try
            {
                return base.SendToConversationAsync(conversationId, activity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
