using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace MonogameAssault.Camera2d;
internal sealed class CameraHelper
{
    internal static Matrix CameraMatrix;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref Vector2 GetTopPositionScreen(ref Vector2 topPositionWindows, ref readonly Matrix cameraMatrix)
    {
        topPositionWindows.X = -cameraMatrix.Translation.X + 1;
        topPositionWindows.Y = -cameraMatrix.Translation.Y + 1;
        return ref topPositionWindows;
    }
}
