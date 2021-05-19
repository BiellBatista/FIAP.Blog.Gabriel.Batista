using System.Collections.Generic;
using System.Diagnostics;
using FIAP.Blog.Gabriel.Batista.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FIAP.Blog.Gabriel.Batista.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController (ILogger<HomeController> logger) {
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
            var posts = new List<BlogPost> () {
                new BlogPost {
                PostId = 1, Title =
                "xxx", ShortDescription = "xxx"
                },
                new BlogPost {
                PostId = 2, Title =
                "xxx", ShortDescription = "xxx"
                },
                new BlogPost {
                PostId = 3, Title =
                "xxx", ShortDescription = "xxx"
                },
                new BlogPost {
                PostId = 4, Title =
                "xxx", ShortDescription = "xxx"
                },
                new BlogPost {
                PostId = 5, Title =
                "xxx", ShortDescription = "xxx"
                }
            };

            return Json (posts);
        }
    }
}