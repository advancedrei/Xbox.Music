
namespace Xbox.Music
{

    /// <summary>
    /// The available options for creating Deep Links.
    /// </summary>
    public enum LinkAction
    {
        /// <summary>
        /// Default. Launches the content details view.
        /// </summary>
        View,

        /// <summary>
        /// Launches playback of the media content.
        /// </summary>
        Play,

        /// <summary>
        /// Opens the "add to collection" screen on the Xbox Music service.
        /// </summary>
        AddToCollection,

        /// <summary>
        /// Opens the appropriate purchase flow on the Xbox Music service.
        /// </summary>
        Buy
    }
}
