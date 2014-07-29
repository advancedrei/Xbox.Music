using System.Net.Http;
using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// Describes errors across all Xbox Music APIs.
    /// </summary>
    /// <remarks>
    /// Documentation found at http://msdn.microsoft.com/en-us/library/dn546683.aspx
    /// </remarks>
    [DataContract]
    public class Error
    {

        /// <summary>
        /// The error code, as described in the following table of error codes.
        /// </summary>
        [DataMember]
        public string ErrorCode { get; set; }


        /// <summary>
        /// A user-friendly description of the error code.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// A more contextual message describing what may have gone wrong.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// The raw HTTP response from the service.
        /// </summary>
        [DataMember]
        public HttpResponseMessage Response { get; set; }

    }
}
