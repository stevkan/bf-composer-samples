using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class JobRouterComposerComponent : BotComponent
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMiddleware, RegisterClassMiddleware<ILoggerFactory>>(sp => new RegisterClassMiddleware<ILoggerFactory>(sp.GetRequiredService<ILoggerFactory>()));

        services.AddSingleton<DeclarativeType>(sp => new DeclarativeType<AdvancedPropertyTest>(nameof(AdvancedPropertyTest)));
    }
}