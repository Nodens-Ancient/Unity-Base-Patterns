using static ObjectPool.ObjectInfo;

public interface IPooledObject
{
    ObjectType Type { get; }
}
