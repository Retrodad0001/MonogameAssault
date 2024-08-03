using Microsoft.Xna.Framework;
using MonogameAssault.Components;
using System;

namespace MonogameAssault;

internal sealed class EntityManager
{
    internal const int EnemyCount = 150000;
    //TODO private Animator[] animators;
    internal bool[] enabled;
    internal Capability[] capabilities;
    internal Vector2[] Positions;

    public EntityManager()
    {

        enabled = new bool[EnemyCount];
        capabilities = new Capability[EnemyCount];
        Positions = new Vector2[EnemyCount];


        for (int i = 0; i < EnemyCount; i++)
        {
            int x = AssaultGame.Random.Next(0, AssaultGame.SCREEN_WIDTH);
            int y = AssaultGame.Random.Next(0, AssaultGame.SCREEN_HEIGHT);

            enabled[i] = true;
            capabilities[i] = Capability.CanMove;
            Positions[i] = new Vector2(x, y);
        }
        //TODO animators = new Animator[enemyCount];
    }

    public void Update(GameTime gameTime)
    {
        UpdatePositions(ref enabled, ref capabilities, ref Positions, gameTime);
    }

    private static void UpdatePositions(ref bool[] enabled
        , ref Capability[] capabilities
        , ref Vector2[] positions
        , GameTime gameTime)
    {
        ReadOnlySpan<bool> enabledSpan = new Span<bool>(enabled);
        ReadOnlySpan<Capability> capabilitiesSpan = new Span<Capability>(capabilities);
        Span<Vector2> positionSpan = new Span<Vector2>(positions);

        float gameTimeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        for (int i = 0; i < EnemyCount; i++)
        {
            if (!enabledSpan[i])
                continue;

            if (capabilitiesSpan[i] != Capability.CanMove)
                continue;

            //50% move left, 50% move right
            if (AssaultGame.Random.Next(0, 2) == 0)
                positionSpan[i].X -= 8 * gameTimeElapsed;
            else
                positionSpan[i].X += 16 * gameTimeElapsed;
        }
    }
}