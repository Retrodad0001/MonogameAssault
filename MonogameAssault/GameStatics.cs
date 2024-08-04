using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace MonogameAssault;
internal static class GameStatics
{
    internal class Windows
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

        internal static Matrix CameraMatrix;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref Vector2 GetTopPositionScreen(ref Vector2 topPositionWindows) //TODO move to some kinds of vast camera class or helper ?
        {
            topPositionWindows.X = -CameraMatrix.Translation.X + 1;
            topPositionWindows.Y = -CameraMatrix.Translation.Y + 1;
            return ref topPositionWindows;
        }

    }
}
