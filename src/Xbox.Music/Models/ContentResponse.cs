using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// The root object returned by the service.
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn546681.aspx
    /// </remarks>
    [DataContract]
    public class ContentResponse
    {

        /// <summary>
        /// A paginated list of Artists that matched the request criteria.
        /// </summary>
        [DataMember]
        public PaginatedList<Artist> Artists { get; set; }

        /// <summary>
        /// A paginated list of Albums that matched the request criteria.
        /// </summary>
        [DataMember]
        public PaginatedList<Album> Albums { get; set; }

        /// <summary>
        /// A paginated list of Tracks that matched the request criteria.
        /// </summary>
        [DataMember]
        public PaginatedList<Track> Tracks { get; set; }

        /// <summary>
        /// A paginated list of Playlists that matched the request criteria.
        /// </summary>
        [DataMember]
        public PaginatedList<Playlist> Playlists { get; set; }

        /// <summary>
        /// A paginated list of ContentItems that matched the request criteria. These items are used for ordered lists mixing multiple types of content such as the Spotlight and NewReleases APIs.
        /// </summary>
        [DataMember]
        public PaginatedList<ContentItem> Results { get; set; }

        /// <summary>
        /// A list of string representing the different possible genres for a given locale. Used in the Browse Genres API.
        /// </summary>
        [DataMember]
        public List<string> Genres { get; set; }

        /// <summary>
        /// The culture used for processing the Browse Genres request, computed from Country and Language parameters, user authentication and/or geolocation.
        /// </summary>
        [DataMember]
        public List<string> Culture { get; set; }
        
        /// <summary>
        /// Optional error.
        /// </summary>
        [DataMember]
        public Error Error { get; set; }

    }
}
