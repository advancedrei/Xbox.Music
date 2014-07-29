using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xbox.Music
{

    /// <summary>
    /// An action (such as add or delete) on a specific track. 
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn754102.aspx
    /// </remarks>
    [DataContract]
    public class TrackAction
    {

        /// <summary>
        /// ID of the track on which the action should be performed. (You can get the ID by browsing the catalog or collection.)
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Action to perform (currently supports only "add" and "delete").
        /// </summary>
        [DataMember]
        public string Action { get; set; }

    }
}
