using Microsoft.Xna.Framework;
using System;

namespace MonogameAssault;

internal class EntityManager
{
    internal const int EnemyCount = 150000;
    //TODO private Animator[] animators;
    internal Vector2[] Positions;

    public EntityManager()
    {
        Positions = new Vector2[EnemyCount];

        for (int i = 0; i < EnemyCount; i++)
        {
            int x = AssaultGame.Random.Next(0, AssaultGame.SCREEN_WIDTH);
            int y = AssaultGame.Random.Next(0, AssaultGame.SCREEN_HEIGHT);

            Positions[i] = new Vector2(x, y);
        }
        //TODO animators = new Animator[enemyCount];
    }

    public void Update(GameTime gameTime)
    {
        Span<Vector2> positionSpan = new Span<Vector2>(Positions);

        UpdatePositions(ref positionSpan, gameTime);
    }

    private static void UpdatePositions(ref Span<Vector2> positions, GameTime gameTime)
    {
        float gameTimeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        for (int i = 0; i < EnemyCount; i++)
        {
            //50% move left, 50% move right
            if (AssaultGame.Random.Next(0, 2) == 0)
                positions[i].X -= 8 * gameTimeElapsed;
            else
                positions[i].X += 16 * gameTimeElapsed;
        }
    }
}