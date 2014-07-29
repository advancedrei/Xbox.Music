using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// New base class for all Xbox Music API response items, as Microsoft added the ContentItem object, which breaks the PaginatedList generic constraint.
    /// </summary>
    [KnownType(typeof(Album))]
    [KnownType(typeof(Artist))]
    [KnownType(typeof(Track))]
    [KnownType(typeof(EntryBase))]
    [DataContract]
    public class MusicItemBase
    {
    }
}
