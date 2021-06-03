using System;
using FIAP.Blog.Gabriel.Batista.Store;

namespace FIAP.Blog.Gabriel.Batista.Services {
    public interface IPushSubscriptionStoreAccessor : IDisposable {
        IPushSubscriptionStore PushSubscriptionStore { get; }
    }
}