using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// A list of tracks and their metadata. 
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn754097.aspx
    /// </remarks>
    [DataContract]
    public class Playlist : EntryBase
    {

        #region Properties

        /// <summary>
        /// Number of tracks currently in the playlist.
        /// </summary>
        [DataMember]
        public int TrackCount { get; set; }

        /// <summary>
        /// A short description of the playlist.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gamertag of the owner of the playlist. 
        /// </summary>
        [DataMember]
        public string Owner { get; set; }

        /// <summary>
        /// Whether the playlist can be edited or not.
        /// </summary>
        [DataMember]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Whether the playlist is publicly visible or not.
        /// </summary>
        [DataMember]
        public bool IsPublished { get; set; }

        /// <summary>
        /// WWhether the current user is the actual owner of the playlist.
        /// </summary>
        [DataMember]
        public bool UserIsOwner { get; set; }

        /// <summary>
        /// A paginated list of the tracks in the playlist. 
        /// </summary>
        /// <remarks>
        /// In case of a browse, this list is null, and you'll need a lookup on that playlist to get its tracks.
        /// </remarks>
        [DataMember]
        public PaginatedList<Track> Tracks { get; set; }

        #endregion

        #region Public Methods


        #endregion


    }
}