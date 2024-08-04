namespace MonogameAssault.Events;
internal readonly ref struct EventCollision
{
    public readonly uint HitByEntity;
    public readonly uint HittedEntity;

    public EventCollision(uint hitByEntity, uint hittedEntity)
    {
        HitByEntity = hitByEntity;
        HittedEntity = hittedEntity;
    }
}
