using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xbox.Music.Tests
{
    [TestClass]
    public class MusicClientTests
    {

        private static string _find1ContinuationToken = "";
        private static string _get1ContinuationToken = "";

        [TestMethod]
        public async Task FindTest1()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Find("Daft Punk");
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Albums);
            Assert.IsNotNull(result.Artists);
            Assert.IsNotNull(result.Tracks);
            
            _find1ContinuationToken = result.Albums.ContinuationToken;
        }

        [TestMethod]
        public async Task FindTest1Continued()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Find("Daft Punk", _find1ContinuationToken);
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Albums);
            Assert.IsNull(result.Artists);
            Assert.IsNull(result.Tracks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FindTest2()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Find("Daft Punk", getAlbums: false);
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Artists);
            Assert.IsNotNull(result.Tracks);
            Assert.IsNull(result.Albums);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FindTest3()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Find("caparezza non me lo posso promettere");
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.IsNotNull(result.Error.Response);
            Assert.AreEqual("NotFound", result.Error.ErrorCode);
        }

        [TestMethod]
        public async Task GetTest1()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Get("music.C61C0000-0200-11DB-89CA-0019B92A3933");
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Artists);
            //Assert.IsNotNull(result.Tracks);
            //Assert.IsNotNull(result.Albums);
        }

        [TestMethod]
        public async Task GetTest2()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var ids = new List<string>
            {
                "music.C61C0000-0200-11DB-89CA-0019B92A3933",
                "music.A83EB907-0100-11DB-89CA-0019B92A3933",
                "music.B13EB907-0100-11DB-89CA-0019B92A3933"
            };
            var result = await client.Get(ids);
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Artists);
            Assert.IsNotNull(result.Tracks);
            Assert.IsNotNull(result.Albums);
        }

        //[TestMethod]
        //public async Task GetTest1Continued()
        //{
        //    var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
        //    var result = await client.Get("music.C61C0000-0200-11DB-89CA-0019B92A3933");
        //    Assert.IsNotNull(result);
        //    Assert.IsNull(result.Error);
        //    Assert.IsNotNull(result.Artists);
        //    //Assert.IsNotNull(result.Tracks);
        //    //Assert.IsNotNull(result.Albums);
        //}


        [TestMethod]
        public async Task PublishedSdkVersionTest()
        {
            var client = new HttpClient();

            // Define the data needed to request an authorization token.
            var service = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            var clientId = "XboxMusicClientTests";
            var clientSecret = "ThisWillBeChangedOften";
            var scope = "http://music.xboxlive.com";
            var grantType = "client_credentials";

            // Create the request data.
            var requestData = new Dictionary<string, string>();
            requestData["client_id"] = clientId;
            requestData["client_secret"] = clientSecret;
            requestData["scope"] = scope;
            requestData["grant_type"] = grantType;

            // Post the request and retrieve the response.
            var response = await client.PostAsync(new Uri(service), new FormUrlEncodedContent(requestData));
            var responseString = await response.Content.ReadAsStringAsync();
            var token = Regex.Match(responseString, ".*\"access_token\":\"(.*?)\".*", RegexOptions.IgnoreCase).Groups[1].Value;

            // Use the token in a new request.
            service = "https://music.xboxlive.com/1/content/music/search?q=daft+punk&accessToken=Bearer+";
            response = await client.GetAsync(new Uri(service + WebUtility.UrlEncode(token)));
            responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);
        }


    }
}
