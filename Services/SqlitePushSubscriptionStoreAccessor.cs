using FIAP.Blog.Gabriel.Batista.Store;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Blog.Gabriel.Batista.Services {
    public class SqlitePushSubscriptionStoreAccessor : IPushSubscriptionStoreAccessor {
        public IPushSubscriptionStore PushSubscriptionStore { get; set; }
        private IServiceScope _serviceScope;
        public SqlitePushSubscriptionStoreAccessor (IServiceScope serviceScope) {
            _serviceScope = serviceScope;
            PushSubscriptionStore = _serviceScope.ServiceProvider.GetService<IPushSubscriptionStore> ();
        }

        public SqlitePushSubscriptionStoreAccessor (IPushSubscriptionStore pushSubscriptionStore) {
            PushSubscriptionStore = pushSubscriptionStore;
        }

        public void Dispose () {
            PushSubscriptionStore = null;
            if (!(_serviceScope is null)) {
                _serviceScope.Dispose ();
                _serviceScope = null;
            }
        }
    }
}