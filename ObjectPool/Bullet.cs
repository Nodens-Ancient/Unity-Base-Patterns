using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public ObjectPool.ObjectInfo.ObjectType Type => type;

    [SerializeField]
    private ObjectPool.ObjectInfo.ObjectType type;

    private float LifeTime = 3f;
    private float CurrentLifetime;
    private float speed;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if ((CurrentLifetime -= LifeTime) < 0)
            ObjectPool.Instance.DestroyObject(gameObject);
    }

    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        CurrentLifetime = LifeTime;
    }
}
