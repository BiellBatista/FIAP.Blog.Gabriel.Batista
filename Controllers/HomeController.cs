using System.Diagnostics;
using FIAP.Blog.Gabriel.Batista.Models;
using FIAP.Blog.Gabriel.Batista.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FIAP.Blog.Gabriel.Batista.Controllers {
    public class HomeController : Controller {
        private readonly IBlogService _blogService;
        private readonly ILogger<HomeController> _logger;

        public HomeController (IBlogService blogService, ILogger<HomeController> logger) {
            _blogService = blogService;
            _logger = logger;
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
    }
}