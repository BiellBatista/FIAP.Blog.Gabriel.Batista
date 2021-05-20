using System.Collections.Generic;
using FIAP.Blog.Gabriel.Batista.Models;

namespace FIAP.Blog.Gabriel.Batista.Services {
    public interface IBlogService {
        List<BlogPost> GetLatestPosts ();
        string GetPostText (string link);

        List<BlogPost> GetOlderPosts (int oldestPostId);
    }
}