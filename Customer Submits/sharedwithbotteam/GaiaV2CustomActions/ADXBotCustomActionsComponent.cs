using GaiaV2CustomActions.Common;
using GaiaV2CustomActions.CustomActions;
using GaiaV2CustomActions.Middleware;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GaiaV2CustomActions
{
    /// <summary>
    /// Customs actions registration 
    /// </summary>
    public class GaiaV2CustomActionsComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMiddleware, TurnStateSetupMiddleware>();
            services.AddSingleton<DeclarativeType>(sp => new DeclarativeType<ClusterInformation>(ClusterInformation.Kind));
            services.AddSingleton<DeclarativeType>(sp => new DeclarativeType<CheckClusterExistance>(CheckClusterExistance.Kind));
        }
    }
}
