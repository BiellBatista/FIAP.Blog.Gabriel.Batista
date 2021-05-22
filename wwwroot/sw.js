"use strict";
importScripts("lib/localforage/localforage.min.js");

var cacheName = "v1Cache";
var blogCacheFiles = [
  "/",
  //arquivos basicos da pwa
  "/sw.js",
  "/lib/bootstrap/dist/css/bootstrap.css",
  "/css/site.css",
  "/lib/jquery/dist/jquery.js",
  "/lib/bootstrap/dist/js/bootstrap.bundle.js",
  "/lib/es6-promise/es6-promise.js",
  "/lib/fetch/fetch.js",
  "/lib/systemjs/system.js",
  "/lib/localforage/localforage.min.js",
  "/lib/localforage/localforage-getitems.js",
  "/lib/localforage/localforage-setitems.js",
  "/js/site.js",
  "/js/app.js",
  "/manifest.json",
  "/favicon.ico",
  "/js/blogService.js",
  "/js/swRegister.js",
  "/js/template.js",
  "/lib/showdown/showdown.js",
  "/js/clientStorage.js",
  "/images/icons/icon-72x72.png",
  "/images/icons/icon-96x96.png",
  "/images/icons/icon-128x128.png",
  "/images/icons/icon-152x152.png",
  "/images/icons/icon-192x192.png",
  "/images/icons/icon-384x384.png",
  "/images/icons/icon-512x512.png",
];

function timeout(ms, promise) {
  return new Promise(function (resolve, reject) {
    setTimeout(function () {
      reject();
    }, ms);
    promise.then(resolve, reject);
  });
}

//Installing
//Pre-cache App Shell
self.addEventListener("install", function (event) {
  console.log("SW: Evento de Instalacao");
  self.skipWaiting();
});

//Activating
//Clean up
self.addEventListener("activate", function (event) {
  console.log("SW: Evento de Ativacao");
  self.clients.claim();
  event.waitUntil(
    caches.keys().then(function (cacheKeys) {
      var deletePromises = [];
      for (var i = 0; i < cacheKeys.length; i++) {
        if (cacheKeys[i] != cacheName) {
          deletePromises.push(caches.delete(cacheKeys[i]));
        }
      }
      return Promise.all(deletePromises);
    })
  );
});

self.addEventListener("fetch", (event) => {
  console.log("url request: " + event.request.url);

  if (
    event.request.url.toLowerCase().includes("/home") ||
    event.request.url.toLowerCase() === "/subscriptions" ||
    event.request.url.toLowerCase() === "/notifications"
  ) {
    console.log("[ServiceWorker] online - get online " + event.request.url);
    event.respondWith(fetch(event.request));
  } else {
    event.respondWith(
      timeout(500, fetch(event.request)).catch(function () {
        console.log(
          "[ServiceWorker] offline - get from cache: " + event.request.url
        );
        return caches.match(event.request);
      })
    );
  }
});

self.addEventListener("backgroundfetchsuccess", (event) => {
  const bgFetch = event.registration;
  event.waitUntil(
    (async function () {
      var blogInstance = localforage.createInstance({
        name: "blog",
      });
      const records = await bgFetch.matchAll();
      const promises = records.map(async (record) => {
        const response = await record.responseReady;
        response.text().then(function (text) {
          console.log("text retrieved");
          blogInstance.setItem("#" + bgFetch.id, text);
        });
      });
      await Promise.all(promises);
      event.updateUI({ title: "Done!" });
    })()
  );
});
