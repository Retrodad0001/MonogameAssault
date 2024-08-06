using System;

namespace MonogameAssault.Components;
[Flags]
internal enum EntityState
{
    NoneActive = 0,
    Active = 1,
    MarkedForNonActive = 2, //At the end of the frame make this entity non active
}
