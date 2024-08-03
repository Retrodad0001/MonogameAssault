using Microsoft.Xna.Framework;
using MonogameAssault.Components;
using System;

namespace MonogameAssault;

internal sealed class EntityManager
{
    internal const int EnemyCount = 150000;
    internal EnityState[] entityStates;
    internal Capability[] capabilities;
    internal Vector2[] Positions;

    //TODO add camera and matrix scaling
    //TODO add some spatial partitioning data structure and only update collidable entities in same partition

    public EntityManager()
    {

        entityStates = new EnityState[EnemyCount];
        capabilities = new Capability[EnemyCount];
        Positions = new Vector2[EnemyCount];


        for (int i = 0; i < EnemyCount; i++)
        {
            int x = AssaultGame.Random.Next(0, AssaultGame.SCREEN_WIDTH);
            int y = AssaultGame.Random.Next(0, AssaultGame.SCREEN_HEIGHT);

            entityStates[i] = EnityState.Active;
            capabilities[i] = Capability.CanMove;
            Positions[i] = new Vector2(x, y);
        }
    }

    public void Update(GameTime gameTime)
    {
        UpdatePositions(ref entityStates, ref capabilities, ref Positions, gameTime);
    }


    private static void UpdatePositions(ref EnityState[] enityStates
        , ref Capability[] capabilities
        , ref Vector2[] positions
        , GameTime gameTime)
    {
        ReadOnlySpan<EnityState> entityStatesSpan = new Span<EnityState>(enityStates);
        ReadOnlySpan<Capability> capabilitiesSpan = new Span<Capability>(capabilities);
        Span<Vector2> positionSpan = new Span<Vector2>(positions);

        float gameTimeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        for (int i = 0; i < EnemyCount; i++)
        {
            if (entityStatesSpan[i] != EnityState.Active)
                continue;

            if (capabilitiesSpan[i] != Capability.CanMove)
                continue;

            //50% move left, 50% move right for the sake of randomness
            if (AssaultGame.Random.Next(0, 2) == 0)
                positionSpan[i].X -= 8 * gameTimeElapsed;
            else
                positionSpan[i].X += 16 * gameTimeElapsed;
        }
    }
}