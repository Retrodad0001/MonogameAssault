using System;

namespace MonogameAssault.Components;

[Flags]
public enum Capability
{
    CanMove = 1,
    CanCollide = 2,
    CanAnimate = 4,
}
