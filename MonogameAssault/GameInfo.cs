namespace MonogameAssault;
internal static class GameInfo
{
    internal sealed class Windows
    {
        /// <summary>
        /// The desired resolution width
        /// </summary>
        internal const int DRAW_RESOLUTION_WIDTH = 480;

        /// <summary>
        /// The desired resolution height
        /// </summary>
        internal const int DRAW_RESOLUTION_HEIGHT = 270;

        /// <summary>
        /// The resolution the game renders at Width (window)
        /// </summary>
        internal static int VIRTUAL_RESOLUTION_WIDTH = 1920;

        /// <summary>
        /// The resolution the game renders at Height (window)
        /// </summary>
        internal static int VIRTUAL_RESLUTION_HEIGHT = 1080;
    }
}
