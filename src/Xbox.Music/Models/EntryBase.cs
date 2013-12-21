using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xbox.Music
{

    /// <summary>
    /// The base object from which the main Xbox Music types inherit.
    /// </summary>
    [KnownType(typeof(Album))]
    [KnownType(typeof(Artist))]
    [KnownType(typeof(Track))]
    [DataContract]
    public class EntryBase
    {

        #region Properties

        /// <summary>
        /// Identifier for this piece of content. All IDs are of the form {namespace}.{actual identifier} 
        /// and may be used in lookup requests.
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// The name of this piece of content.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// A direct link to the default image associated with this piece of content.
        /// </summary>
        [DataMember]
        public string ImageUrl { get; set; }

        /// <summary>
        /// A music.xbox.com link that redirects to a contextual page for this piece of content on the 
        /// relevant Xbox Music client application depending on the user's device or operating system.
        /// </summary>
        [DataMember]
        public string Link { get; set; }

        /// <summary>
        /// An optional collection of other IDs that identify this piece of content on top of the main ID. 
        /// Each key is the namespace or subnamespace in which the ID belongs, and each value is a secondary ID for this piece of content.
        /// </summary>
        [DataMember]
        public Dictionary<string, string> OtherIds { get; set; }

        /// <summary>
        /// An indication of the data source for this piece of content. Currently only "Catalog" is supported.
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a resized image based on the formatting criteria allowed by the Xbox Music service.
        /// </summary>
        /// <param name="width">Image width in pixels.</param>
        /// <param name="height">Image height in pixels.</param>
        /// <param name="mode">The mode with wich to resize the image.</param>
        /// <param name="backgroundColor">HTML-compliant color for letterbox resize mode background. Defaults to an empty string.</param>
        /// <returns></returns>
        public string GetImage(int width, int height, ImageResizeMode mode = ImageResizeMode.Crop, string backgroundColor = "")
        {
            var modeString = Enum.GetName(typeof (ImageResizeMode), mode).ToLower();
            return string.Format("{0}&w={1}&h={2}&mode={3}&background={4}", ImageUrl, width, height, modeString, backgroundColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        internal string GetDeepLinkInternal(string entity, LinkAction action = LinkAction.View)
        {
            var actionString = Enum.GetName(typeof(LinkAction), action).ToLower();
            return string.Format("http://music.xbox.com/{0}/{1}?action={2}", entity, Id.Replace("music.", ""), actionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliateId"></param>
        /// <param name="unescapedDeepLink"></param>
        /// <returns></returns>
        internal string GetDeepLinkInternal(string affiliateId, string unescapedDeepLink)
        {
            var deepLink = Uri.EscapeUriString(unescapedDeepLink);
            return string.Format("http://click.linksynergy.com/deeplink?id={0}&mid=39033&murl={1}", affiliateId, deepLink);
        }


        #endregion



    }
}
