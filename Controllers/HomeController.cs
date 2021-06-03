using System.Diagnostics;
using System.Threading.Tasks;
using FIAP.Blog.Gabriel.Batista.Models;
using FIAP.Blog.Gabriel.Batista.Models.Requests;
using FIAP.Blog.Gabriel.Batista.Services;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Blog.Gabriel.Batista.Controllers {
    public class HomeController : Controller {
        private IBlogService _blogService;

        private readonly IPushService _pushService;

        public HomeController (IBlogService blogService, IPushService pushService) {
            _blogService = blogService;
            _pushService = pushService;
        }

        public IActionResult Index () {
            return View ();
        }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult LatestBlogPosts () {
            var posts = _blogService.GetLatestPosts ();

            return Json (posts);
        }

        public ContentResult Post (string link) {
            return Content (_blogService.GetPostText (link));
        }

        public JsonResult MoreBlogPosts (int oldestBlogPostId) {
            var posts = _blogService.GetOlderPosts (oldestBlogPostId);
            return Json (posts);
        }

        [HttpGet ("publickey")]
        public ContentResult GetPublicKey () {
            return Content (_pushService.GetKey (), "text/plain");
        }

        //armazena subscricoes
        [HttpPost ("subscriptions")]
        public async Task<IActionResult> StoreSubscription ([FromBody] PushSubscription subscription) {
            int res = await _pushService.StoreSubscriptionAsync (subscription);

            if (res > 0)
                return CreatedAtAction (nameof (StoreSubscription), subscription);

            return NoContent ();
        }

        [HttpDelete ("subscriptions")]
        public async Task<IActionResult> DiscardSubscription (string endpoint) {
            await _pushService.DiscardSubscriptionAsync (endpoint);

            return NoContent ();
        }

        [HttpPost ("notifications")]
        public IActionResult SendNotification ([FromBody] PushMessageViewModel messageVM) {
            var message = new PushMessage (messageVM.Notification) {
                Topic = messageVM.Topic,
                Urgency = messageVM.Urgency
            };

            _pushService.SendNotificationAsync (message);

            return NoContent ();
        }

        [HttpPost ("Pay")]
        public ActionResult Pay (PayRequest payment) {
            return Ok ();
        }
    }
}