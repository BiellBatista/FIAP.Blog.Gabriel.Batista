using System.Text.RegularExpressions;

namespace FIAP.Blog.Gabriel.Batista.Extensions {
    public static class UrlExtensions {
        public static string UrlFriendly (this string text) {
            return Regex.Replace (text, @"[^A-Za-z0-9_\.~]+", "-");
        }
    }
}