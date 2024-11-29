using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using tgBotOrderV11.TgBot.Body;
using tgBotOrderV11.Repos;
using tgBotOrderV11.TgBot.Abstract;
using tgBotOrderV11.Utils;
using tgBotOrderV11.DbBot;
using Microsoft.Extensions.Caching.Memory;
using tgBotOrderV11.Resos;
using tgBotOrderV11.TgBot.TgLogic.MachineState;
using tgBotOrderV11;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register Bot configuration
        services.Configure<BotConfiguration>(context.Configuration.GetSection("BotConfiguration"));
        services.AddHttpClient("telegram_bot_client").RemoveAllLoggers()
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfiguration = sp.GetService<IOptions<BotConfiguration>>()?.Value;
                    ArgumentNullException.ThrowIfNull(botConfiguration);
                    TelegramBotClientOptions options = new(botConfiguration.BotToken)

                    return new TelegramBotClient(options, httpClient);
                });
        services.AddDbContext<TgBotOrderContext>();
        services.AddMemoryCache();
        services.AddScoped<UpdateHandler>();
        services.AddScoped<BlacklistRepos>();
        services.AddScoped<WalletRepos>();
        services.AddScoped<UserRepos>();
        services.AddScoped<ReferalRepos>();
        services.AddScoped<Resositories>();
        services.AddScoped<StateController>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();
