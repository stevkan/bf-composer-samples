using GaiaV2CustomActions.Common;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GaiaV2CustomActions.Middleware
{
    /// <summary>
    /// Class to setup state used by dialogs
    /// </summary>
    public class TurnStateSetupMiddleware : IMiddleware
    {
        #region Private members
        private readonly IBotSettings m_botSettings;
        private readonly UserState m_userState;
        private readonly IBot m_bot;
        #endregion

        #region Constructor
        public TurnStateSetupMiddleware(IBot bot, IConfiguration configuration, UserState userState)
        {
            m_bot = bot;
            m_botSettings = new BotSettings(configuration);
            m_userState = userState;
        }
        #endregion // Constructor

        #region Public methods
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            var token = await GetToken(turnContext, cancellationToken).ConfigureAwait(false);
            if (turnContext.Activity.Type != ActivityTypes.ConversationUpdate && string.IsNullOrEmpty(token) && !IsTeamsVerificationInvoke(turnContext))
            {
                //turnContext.Activity.Type = ActivityTypes.Message;
                turnContext.Activity.Name = "FORCE_SIGNIN";
                turnContext.Activity.Value = "FORCE_SIGNIN";
                turnContext.Activity.Text = "FORCESIGNIN";
            }

            await GetOrUpdateClientsStateFromStorage(turnContext, token).ConfigureAwait(false);
            await next(cancellationToken).ConfigureAwait(false);
            await GetOrUpdateClientsStateFromStorage(turnContext, token, true).ConfigureAwait(false);
        }

        #endregion // Public methods

        private async Task GetOrUpdateClientsStateFromStorage(ITurnContext turnContext, string token, bool update = false)
        {
            if(turnContext.Activity.Type == ActivityTypes.Message && turnContext.Activity.Value != "FORCESIGNIN" && !string.IsNullOrEmpty(token))
            {
                if(update)
                {
                    await SetClientsStateToStorage(turnContext, m_userState).ConfigureAwait(false);
                }
                else
                {
                    await LoadClientsStateFromStorage(turnContext, m_userState).ConfigureAwait(false);
                }
            }
        }

        private async Task LoadClientsStateFromStorage(ITurnContext turnContext, UserState userState)
        {
            var userStateAccessors = CreateProperty(userState);
            var clients = await userStateAccessors.GetAsync(turnContext, () => new Clients()).ConfigureAwait(false);
            var clientActivityId = turnContext.Activity.ChannelId + "-" + turnContext.Activity.Conversation.Id;
            if (clients != null)
            {
                clients?.Initialize(m_botSettings, clientActivityId);
                turnContext.TurnState.Add(clients); // Add to turn state to make it available for custom actions
            }
            else
            {
                // error
                //PrivateTracer.Tracer.TraceError($"Exception in retrieving clients state from user state with client activity id {clientActivityId}");
            }
        }

        private async Task SetClientsStateToStorage(ITurnContext turnContext, UserState userState, CancellationToken cancellationToken = default)
        {
            var clients = turnContext.TurnState.Get<IClients>();
            var clientActivityId = turnContext.Activity.ChannelId + "-" + turnContext.Activity.Conversation.Id;
            if (clients != null)
            {
                var userStateAccessors = CreateProperty(userState);
                await userStateAccessors.SetAsync(turnContext, clients).ConfigureAwait(false);
                await userState.SaveChangesAsync(turnContext, false, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                // error
                //PrivateTracer.Tracer.TraceError($"Exception in saving clients state to user state with client activity id {clientActivityId}");
            }
        }

        private IStatePropertyAccessor<IClients> CreateProperty(UserState userState)
        {
            return userState.CreateProperty<IClients>(nameof(IClients));
        }

        private async Task<string> GetToken(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            string token = null;
            if (IsTeamsVerificationInvoke(turnContext))
            {
                var magicCodeObject = turnContext.Activity.Value as JObject;
                var magicCode = magicCodeObject.GetValue("state", StringComparison.Ordinal)?.ToString();
                token = await GetAuthToken(turnContext, magicCode, cancellationToken).ConfigureAwait(false);
            }
            else if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // regex to check if code supplied is a 6 digit numerical code (hence, a magic code).
                var magicCodeRegex = new Regex(@"(\d{6})");
                var matched = magicCodeRegex.Match(turnContext.Activity.Text);
                var magicCode = matched.Success ? matched.Value : null;
                token = await GetAuthToken(turnContext, magicCode, cancellationToken).ConfigureAwait(false);
            }
            return token;
        }

        private bool IsTeamsVerificationInvoke(ITurnContext turnContext)
        {
            var activity = turnContext.Activity;
            return activity.Type == ActivityTypes.Invoke && activity.Name == SignInConstants.VerifyStateOperationName;
        }

        private async Task<string> GetAuthToken(ITurnContext turnContext, string magicCode, CancellationToken cancellationToken = default)
        {
            TokenResponse tokenResponse = null;
            var userTokenClient = turnContext.TurnState.Get<UserTokenClient>();
            if (userTokenClient != null)
            {
                tokenResponse = await userTokenClient.GetUserTokenAsync(turnContext.Activity.From?.Id, "oauthconnection", turnContext.Activity.ChannelId, magicCode, cancellationToken).ConfigureAwait(false);
            }
            return tokenResponse?.Token; // TBD - do we need to verify tokenResponse?.Expiration
        }

        #region Tracer
        //private class PrivateTracer : TraceSourceBase<PrivateTracer>
        //{
        //    public override string Id => "GaiaV2CustomActions.TurnStateSetupMiddleware";
        //    public override TraceVerbosity DefaultVerbosity => TraceVerbosity.Verbose;
        //}
        #endregion
    }
}
