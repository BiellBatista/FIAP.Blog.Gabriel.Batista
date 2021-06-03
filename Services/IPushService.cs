using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Blog.Gabriel.Batista.Services {
    public interface IPushService {
        Task DiscardSubscriptionAsync (string endpoint);
        string GetKey ();
        void SendNotificationAsync (PushMessage message);
        Task<int> StoreSubscriptionAsync ([FromBody] PushSubscription subscription);
    }
}