using FIAP.Blog.Gabriel.Batista.Services;
using FIAP.Blog.Gabriel.Batista.Store;
using Lib.Net.Http.WebPush;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Blog.Gabriel.Batista {
    public static class ServiceCollectionExtensions {
        private const string SQLITE_CONNECTION_STRING_NAME = "Data Source=PushSubscriptionSqliteDatabase";

        public static IServiceCollection AddPushSubscriptionStore (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<PushSubscriptionContext> (options =>
                options.UseSqlite (SQLITE_CONNECTION_STRING_NAME)
            );

            services.AddTransient<IPushService, PushService> ();
            services.AddTransient<IPushSubscriptionStore, SqlitePushSubscriptionStore> ();
            services.AddHttpContextAccessor ();
            services.AddSingleton<IPushSubscriptionStoreAccessorProvider, SqlitePushSubscriptionStoreAccessorProvider> ();

            return services;
        }

        public static IServiceCollection AddPushNotificationService (this IServiceCollection services, IConfiguration configuration) {
            services.AddOptions ();
            services.AddMemoryCache ();
            services.AddMemoryVapidTokenCache ();
            services.AddPushServiceClient (options => {
                IConfigurationSection pushNotificationServiceConfigurationSection = configuration.GetSection (nameof (PushServiceClient));

                options.Subject = pushNotificationServiceConfigurationSection.GetValue<string> (nameof (options.Subject));
                options.PublicKey = pushNotificationServiceConfigurationSection.GetValue<string> (nameof (options.PublicKey));
                options.PrivateKey = pushNotificationServiceConfigurationSection.GetValue<string> (nameof (options.PrivateKey));
            });

            return services;
        }
    }
}