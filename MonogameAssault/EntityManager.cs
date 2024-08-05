using Microsoft.Xna.Framework;
using MonogameAssault.Components;
using MonogameAssault.CoreComponents;
using System;

namespace MonogameAssault;

internal sealed class EntityManager
{
    internal const uint ENEMY_COUNT = 150000;
    internal EnityState[] entityStates;
    internal ActorKind[] actorKinds;
    internal Capability[] capabilities;
    internal Vector2[] CurrentPositions;

    //TODO add some spatial partitioning data structure and only update collidable entities in same partition

    public EntityManager()
    {
        entityStates = new EnityState[ENEMY_COUNT];
        actorKinds = new ActorKind[ENEMY_COUNT];
        capabilities = new Capability[ENEMY_COUNT];
        CurrentPositions = new Vector2[ENEMY_COUNT];

        for (uint i = 0; i < ENEMY_COUNT; i++)
        {
            int x = AssaultGame.Random.Next(0, GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH);
            int y = AssaultGame.Random.Next(0, GameInfo.Windows.VIRTUAL_RESLUTION_HEIGHT);

            entityStates[i] = EnityState.Active;
            actorKinds[i] = ActorKind.EnemyWasp;
            capabilities[i] = Capability.CanMove;

            CurrentPositions[i] = new Vector2(x, y);
        }
    }

    public void Update(GameTime gameTime)
    {
        float elapsedGametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Span<EnityState> entityStates = new Span<EnityState>(this.entityStates);
        Span<ActorKind> actorKinds = new Span<ActorKind>(this.actorKinds);
        Span<Capability> capabilities = new Span<Capability>(this.capabilities);
        Span<Vector2> CurrentPositions = new Span<Vector2>(this.CurrentPositions);

        UpdatePositions(entityStates, capabilities, CurrentPositions, elapsedGametime);
    }

    private static void UpdatePositions(ReadOnlySpan<EnityState> entityStatesSpan
        , ReadOnlySpan<Capability> capabilitiesSpan
        , Span<Vector2> positionSpan
        , float elapsedGametime)
    {
        for (int i = 0; i < ENEMY_COUNT; i++)
        {
            if (entityStatesSpan[i] != EnityState.Active)
                continue;

            if (capabilitiesSpan[i] != Capability.CanMove)
                continue;

            //50% move left, 50% move right for the sake of randomness
            if (AssaultGame.Random.Next(0, 2) == 0)
                positionSpan[i].X -= 8 * elapsedGametime;
            else
                positionSpan[i].X += 16 * elapsedGametime;
        }
    }
}