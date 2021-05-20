define([
  "./template.js",
  "../lib/showdown/showdown.js",
  "./clientStorage.js",
], function (template, showdown, clientStorage) {
  var blogLatestPostsUrl = "/Home/LatestBlogPosts/";
  var blogPostUrl = "/Home/Post/?link=";
  var blogMorePostsUrl = "/Home/MoreBlogPosts/?oldestBlogPostId=";

  var oldestBlogPostId = 0;

  function fetchPromise(url, link, text) {
    link = link || "";

    return new Promise(function (resolve, reject) {
      fetch(url + link)
        .then(function (data) {
          var resolveSuccess = function () {
            resolve("The connection is OK, showing latest results");
          };

          if (text) {
            data.text().then(function (text) {
              clientStorage.addPostText(link, text).then(resolveSuccess);
            });
          } else {
            data.json().then(function (jsonData) {
              clientStorage.addPosts(jsonData).then(resolveSuccess);
            });
          }
        })
        .catch(function (e) {
          resolve("No connection, showing offline results");
        });

      setTimeout(function () {
        resolve("The connection is hanging, showing offline results");
      }, 800);
    });
  }

  function setOldestBlogPostId(data) {
    var ids = data.map((item) => item.postId);
    oldestBlogPostId = Math.min(...ids);
  }

  function loadData(url) {
    fetchPromise(url).then(function (status) {
      $("#connection-status").html(status);
      clientStorage.getPosts().then(function (posts) {
        template.appendBlogList(posts);
      });
    });
  }

  function loadMoreBlogPosts() {
    loadData(blogMorePostsUrl + clientStorage.getOldestBlogPostId());
  }

  function loadLatestBlogPosts() {
    loadData(blogLatestPostsUrl);
  }

  function loadMoreBlogPosts() {
    loadData(blogMorePostsUrl + oldestBlogPostId);
  }

  function loadBlogPost(link) {
    fetchPromise(blogPostUrl, link, true).then(function (status) {
      $("#connection-status").html(status);
      clientStorage.getPostText(link).then(function (data) {
        if (!data) {
          template.showBlogItem($("#blog-content-not-found").html(), link);
        } else {
          var converter = new showdown.Converter();
          html = converter.makeHtml(data);
          template.showBlogItem(html, link);
        }
        window.location = "#" + link;
      });
    });
  }

  return {
    loadLatestBlogPosts: loadLatestBlogPosts,
    loadBlogPost: loadBlogPost,
    loadMoreBlogPosts: loadMoreBlogPosts,
  };
});
