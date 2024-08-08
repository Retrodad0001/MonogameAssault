using Microsoft.Xna.Framework;
using MonogameAssault.CoreComponents;

namespace TestProject.Components;

public class PartitionTests
{
    [Theory]
    [InlineData(0   //entityX
        , 0         //entityY
        , 0         //expectedPartitionX
        , 0)]       //expectedPartitiony

    [InlineData(100 //entityX
        , 100       //entityY
        , 1         //expectedPartitionX 
        , 1)]       //expectedPartitiony

    [InlineData(99 //entityX
        , 99       //entityY
        , 0         //expectedPartitionX 
        , 0)]       //
    public void CalculatePartitionTest(float entityX
        , float entityY
        , uint expectedPartitionX
        , uint expectedPartitiony)
    {
        //Arrange
        Vector2 entityTransform = new(entityX, entityY);
        uint partitionWH = 100;

        //Act
        Partition partition = new(0, 0);
        Partition actual = Partition.CalculatePartition(ref partition, ref entityTransform, partitionWH);

        //Assert
        Partition expected = new(expectedPartitionX, expectedPartitiony);
        Assert.Equal(expected, actual);
    }
}
