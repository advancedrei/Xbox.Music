using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortableRest;

namespace Xbox.Music
{

    /// <summary>
    /// 
    /// </summary>
    public class MusicClient : RestClient
    {

        #region Properties

        /// <summary>
        /// The Client ID assigned to you from your Azure Marketplace account.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// The Client Secret assigned to you from your Azure Marketplace account.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Optional. The two-letter standard code identifying the requested language for the response content. 
        /// If not specified, defaults to the country's primary language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Optional. The standard two-letter code that identifies the country/region of the user. 
        /// If not specified, the value defaults to the geolocated country/region of the client's IP address. 
        /// Responses will be filtered to provide only those that match the user's country/region.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Required. A valid developer authentication Access Token obtained from Azure Data Market, 
        /// used to identify the third-party application using the Xbox Music RESTful API.
        /// </summary>
        //private string AccessToken { get; set; }

        /// <summary>
        /// Keeps track of when the token was last issued so the MusicClient can obtain a new one 
        /// before it expires.
        /// </summary>
        //private DateTime TokenLastAcquired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private TokenResponse TokenResponse { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the MusicClient for a given ClientId and ClientSecret.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public MusicClient(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            BaseUrl = "https://music.xboxlive.com";

            // PCL-friendly way to get current version
            var thisAssembly = typeof(Artist).GetTypeInfo().Assembly;
            var thisAssemblyName = new AssemblyName(thisAssembly.FullName);
            var thisVersion = thisAssemblyName.Version;

            var prAssembly = typeof(RestRequest).GetTypeInfo().Assembly;
            var prAssemblyName = new AssemblyName(prAssembly.FullName);
            var prVersion = prAssemblyName.Version;

            UserAgent = string.Format("Xbox Music Portable Client {0} (PortableRest {1})", thisVersion, prVersion);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allows you to get an <see cref="Artist"/>/<see cref="Album"/>/<see cref="Track"/> by a known identifier. 
        /// </summary>
        /// <param name="id">The ID to search for. Must start with "music."</param>
        /// <returns>A <see cref="ContentResponse"/> object populated with results from the Xbox Music service.</returns>
        public async Task<ContentResponse> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id", "You must specify an ID");

            await CheckToken();

            var request = GetPopulatedRequest("1/content/{id}/lookup");
            request.AddUrlSegment("id", id);
            request.AddQueryString("accessToken", "Bearer " + TokenResponse.AccessToken);
            
            return await ExecuteAsync<ContentResponse>(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="maxItems"></param>
        /// <returns>A <see cref="ContentResponse"/> object populated with results from the Xbox Music service.</returns>
        /// <exception cref="ArgumentOutOfRangeException">This exception is thrown if you try to ask for more than 25 items.</exception>
        public async Task<ContentResponse> Find(string query, int maxItems = 25, bool getArtists = true, bool getAlbums = true, bool getTracks = true)
        {
            if (maxItems > 25)
                throw new ArgumentOutOfRangeException("maxItems", "Value cannot be greater than 25.");

            await CheckToken();

            var request = GetPopulatedRequest("1/content/{namespace}/search");

            request.AddQueryString("q", query);

            if (maxItems != 25)
            {
                request.AddQueryString("maxItems", maxItems);
            }

            if (!(getArtists && getAlbums && getTracks))
            {
                var filter = new List<string>();

                if (getArtists) filter.Add("artists");
                if (getAlbums) filter.Add("albums");
                if (getTracks) filter.Add("tracks");

                request.AddQueryString("filter", filter.Aggregate("", (c, n) => c.Length == 0 ? c += n : c += "+" + n));
            }

            request.AddQueryString("accessToken", "Bearer " + TokenResponse.AccessToken);

            return await ExecuteAsync<ContentResponse>(request);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        private RestRequest GetPopulatedRequest(string resourceUrl)
        {
            if (string.IsNullOrWhiteSpace(TokenResponse.AccessToken))
            {
                throw new Exception("The Xbox Music Client was unable to obtain an AccessToken from the authentication service.");
            }

            var request = new RestRequest(resourceUrl) { ContentType = ContentTypes.Json };

            request.AddUrlSegment("namespace", "music");

            if (!string.IsNullOrWhiteSpace(Language))
            {
                request.AddQueryString("language", Language);
            }
            if (!string.IsNullOrWhiteSpace(Country))
            {
                request.AddQueryString("country", Country);
            }
           
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task CheckToken()
        {
            if (TokenResponse != null && TokenResponse.NeedsRefresh)
            {
                // RWM: The token is still valid but within the 30 refresh window. 
                // Get a new token, but to not block the existing request.
                Debug.WriteLine("Proactively refreshing the AccessToken...");
                // ReSharper disable once CSharpWaawesonmernings::CS4014
                Authenticate();
            }

            if (TokenResponse == null || !TokenResponse.IsValid)
            {
                // RWM: The token is invalid or outside the refresh window. 
                // Get a new token, blocking the waiting request until the token has been acquired.
                Debug.WriteLine("Obtaining an AccessToken...");
                await Authenticate();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Authenticate()
        {
            var client = new RestClient
            {
                BaseUrl = "https://datamarket.accesscontrol.windows.net",
                UserAgent = UserAgent,
            };
            var request = new RestRequest("v2/OAuth2-13", HttpMethod.Post)
            {
                ContentType = ContentTypes.FormUrlEncoded,
                ReturnRawString = true,
            };
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);
            request.AddParameter("scope", "http://music.xboxlive.com");
            request.AddParameter("grant_type", "client_credentials");

            var result = await client.ExecuteAsync<string>(request);

            TokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
            if (TokenResponse != null)
            {
                TokenResponse.TimeStamp = DateTime.Now;
            }

            //var token = Regex.Match(result, ".*\"access_token\":\"(.*?)\".*", RegexOptions.IgnoreCase).Groups[1].Value;
            //AccessToken = token;
            //TokenLastAcquired = DateTime.Now;
        }

        #endregion

    }
}
