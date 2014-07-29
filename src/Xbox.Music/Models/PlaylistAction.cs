using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xbox.Music
{

    /// <summary>
    /// The input element for every playlist action request: create, update, and delete. 
    /// </summary>
    /// <remarks>
    /// A PlaylistAction contains playlist metadata and a list of actions to perform on the tracks in the playlist.
    /// </remarks>
    [DataContract]
    public class PlaylistAction
    {

        /// <summary>
        /// Required. ID of the playlist (required for update and delete).
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Optional. Name of the playlist.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Optional. Whether the playlist is public or not.
        /// </summary>
        [DataMember]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Optional. List of actions to perform on the playlist's tracks.
        /// </summary>
        [DataMember]
        public List<TrackAction> TrackActions { get; set; }

    }
}
