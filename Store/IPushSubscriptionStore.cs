using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;

namespace FIAP.Blog.Gabriel.Batista.Store {
    public interface IPushSubscriptionStore {
        Task<int> StoreSubscriptionAsync (PushSubscription subscription);

        Task DiscardSubscriptionAsync (string endpoint);

        Task ForEachSubscriptionAsync (Action<PushSubscription> action);

        Task ForEachSubscriptionAsync (Action<PushSubscription> action, CancellationToken cancellationToken);
    }
}