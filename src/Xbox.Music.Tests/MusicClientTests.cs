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
        [TestMethod]
        public async Task FindTest1()
        {
            var client = new MusicClient("XboxMusicClientTests", "ThisWillBeChangedOften");
            var result = await client.Find("Daft Punk");
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Artists);
            Assert.IsNotNull(result.Tracks);
            Assert.IsNotNull(result.Albums);
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
