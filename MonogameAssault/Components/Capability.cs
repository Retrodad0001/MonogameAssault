using System;

namespace MonogameAssault.Components;

[Flags]
internal enum Capability
{
    CanMove = 1,
    CanCollide = 2,
    CanAnimate = 4,
}
