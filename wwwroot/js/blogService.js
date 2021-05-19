define([], function () {
  var blogPostUrl = "/Home/LatestBlogPosts/";
  function loadLatestBlogPosts() {
    fetch(blogPostUrl)
      .then(function (response) {
        return response.json();
      })
      .then(function (data) {
        console.log(data);
      });
  }
  return { loadLatestBlogPosts: loadLatestBlogPosts };
});
