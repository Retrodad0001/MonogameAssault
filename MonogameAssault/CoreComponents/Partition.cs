using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MonogameAssault.CoreComponents;
public struct Partition
{
    /// <summary>
    /// For this example project we have 100 partitions to divide the screen into   0,0     -       1,0     -       2,0     -       ....99,0
    ///                                                                             1,0     -       1,1     -       2,1     -       ....99,1
    ///                                                                             .....
    ///                                                                             .....    
    ///                                                                             99,0    -       99,1    -       99,2    -       ....99,99
    ///                                                                                     
    /// </summary>
    internal uint PartitionX;
    internal uint PartitionY;

    public Partition(uint x, uint y)
    {
        Debug.Assert(x < 100);
        Debug.Assert(y < 100);
        Debug.Assert(x >= 0);
        Debug.Assert(y >= 0);

        PartitionX = x;
        PartitionY = y;
    }

    public static ref Partition CalculatePartition(ref Partition partition
        , ref readonly Vector2 entityTransform
        , uint partititionWH)

    {
        partition.PartitionX = (uint)(entityTransform.X / partititionWH);
        partition.PartitionY = (uint)(entityTransform.Y / partititionWH);

        return ref partition;
    }
}
