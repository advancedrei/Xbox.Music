using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// A musical recording. 
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn546679.aspx
    /// </remarks>
    [DataContract]
    public class Album : EntryBase
    {

        #region Properties

        /// <summary>
        /// Nullable. The album release date.
        /// </summary>
        [DataMember]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Nullable. The album total duration.
        /// </summary>
        [DataMember]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Nullable. The number of tracks on the album.
        /// </summary>
        [DataMember]
        public int? TrackCount { get; set; }

        /// <summary>
        /// Nullable. True if the album contains explicit content.
        /// </summary>
        [DataMember]
        public bool? IsExplicit { get; set; }

        /// <summary>
        /// The name of the music label that produced this album.
        /// </summary>
        [DataMember]
        public string LabelName { get; set; }

        /// <summary>
        /// The list of musical genres associated with this album.
        /// </summary>
        [DataMember]
        public List<string> Genres { get; set; }

        /// <summary>
        /// The list of musical sub-genres associated with this album.
        /// </summary>
        [DataMember]
        public List<string> Subgenres { get; set; }

        /// <summary>
        /// The type of album (for example, Album, Single, and so on).
        /// </summary>
        [DataMember]
        public string AlbumType { get; set; }

        /// <summary>
        /// The album subtitle.
        /// </summary>
        [DataMember]
        public List<string> Subtitle { get; set; }

        /// <summary>
        /// The list of distribution rights associated with this album in Xbox Music (for example, Stream, Purchase, and so on).
        /// </summary>
        [Obsolete("The Rights property appears to have been deprecated.", false)]
        [DataMember]
        public List<string> Rights { get; set; }

        /// <summary>
        /// The list of contributors (artists and their roles) to the album.
        /// </summary>
        [DataMember]
        public List<Contributor> Artists { get; set; }

        /// <summary>
        /// A paginated list of the album's tracks. 
        /// </summary>
        /// <remarks>
        /// This list is null by default unless requested as extra information in a lookup request. If not null, it should 
        /// most often be full without the need to use a continuation token; only a few cases of albums containing a very 
        /// large number of tracks will use pagination. Tracks in this list contain only a few fields, including the ID 
        /// that should be used in a lookup request in order to have the full track properties.
        /// </remarks>
        [DataMember]
        public PaginatedList<Track> Tracks { get; set; }

        #endregion

        #region Public Methods



        #endregion

    }
}