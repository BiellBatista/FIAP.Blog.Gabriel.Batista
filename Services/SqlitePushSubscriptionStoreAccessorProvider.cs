using System;
using FIAP.Blog.Gabriel.Batista.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Blog.Gabriel.Batista.Services {
    public class SqlitePushSubscriptionStoreAccessorProvider : IPushSubscriptionStoreAccessorProvider {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public SqlitePushSubscriptionStoreAccessorProvider (IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider) {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public IPushSubscriptionStoreAccessor GetPushSubscriptionStoreAccessor () {
            if (_httpContextAccessor.HttpContext is null) {
                return new SqlitePushSubscriptionStoreAccessor (_serviceProvider.CreateScope ());
            } else {
                return new SqlitePushSubscriptionStoreAccessor (
                    _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IPushSubscriptionStore> ());
            }
        }
    }
}