using System;
using Microsoft.Xna.Framework;
using MonogameAssault.Components;
using MonogameAssault.CoreComponents;

namespace MonogameAssault;

internal sealed class EntityManager
{
    internal const uint ENEMY_COUNT = 150000;
    internal EntityState[] entityStates;
    internal ActorKind[] actorKinds;
    internal Capability[] capabilities;
    internal Vector2[] CurrentPositions;

    //TODO add some spatial partitioning data structure and only update Collidable entities in same partition

    public EntityManager()
    {
        entityStates = new EntityState[ENEMY_COUNT];
        actorKinds = new ActorKind[ENEMY_COUNT];
        capabilities = new Capability[ENEMY_COUNT];
        CurrentPositions = new Vector2[ENEMY_COUNT];

        for (uint i = 0; i < ENEMY_COUNT; i++)
        {
            int x = AssaultGame.Random.Next(0, GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH);
            int y = AssaultGame.Random.Next(0, GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT);

            entityStates[i] = EntityState.Active;
            actorKinds[i] = ActorKind.EnemyWasp;
            capabilities[i] = Capability.CanMove;

            CurrentPositions[i] = new Vector2(x, y);
        }
    }

    public void Update(GameTime gameTime)
    {
        float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Span<EntityState> entityStates = new Span<EntityState>(this.entityStates);
        Span<ActorKind> actorKinds = new Span<ActorKind>(this.actorKinds);
        Span<Capability> capabilities = new Span<Capability>(this.capabilities);
        Span<Vector2> CurrentPositions = new Span<Vector2>(this.CurrentPositions);

        UpdatePositions(entityStatesSpan: entityStates
        , capabilitiesSpan: capabilities
        , positionSpan: CurrentPositions
        , elapsedGameTime: elapsedGameTime);
    }

    private static void UpdatePositions(ReadOnlySpan<EntityState> entityStatesSpan
        , ReadOnlySpan<Capability> capabilitiesSpan
        , Span<Vector2> positionSpan
        , float elapsedGameTime)
    {
        for (int i = 0; i < ENEMY_COUNT; i++)
        {
            if (entityStatesSpan[i] != EntityState.Active)
                continue;

            if (capabilitiesSpan[i] != Capability.CanMove)
                continue;

            //50% move left, 50% move right for the sake of randomness
            if (AssaultGame.Random.Next(0, 2) == 0)
                positionSpan[i].X -= 8 * elapsedGameTime;
            else
                positionSpan[i].X += 16 * elapsedGameTime;
        }
    }
}
