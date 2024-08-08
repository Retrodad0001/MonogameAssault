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
        internal static int VIRTUAL_RESOLUTION_HEIGHT = 1080;

        internal const uint PARTITION_WH = 100;

        internal const uint MAX_NUMERS_OF_PARTITIONS_WH = 10;

        internal const uint MAX_PARTITIONS_IN_PIXELS_WH = MAX_NUMERS_OF_PARTITIONS_WH * PARTITION_WH;
    }
}
