using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xbox.Music
{

    /// <summary>
    /// An item of content (either an Album or an Artist). 
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn754096.aspx
    /// </remarks>
    [DataContract]
    public class ContentItem : MusicItemBase
    {

        /// <summary>
        /// The type of the content element wrapped in this item.
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Album item if Type is Albums, null otherwise.
        /// </summary>
        [DataMember]
        public Album Album { get; set; }

        /// <summary>
        /// Artist item if Type is Artists, null otherwise.
        /// </summary>
        [DataMember]
        public Artist Artist { get; set; }

    }
}
