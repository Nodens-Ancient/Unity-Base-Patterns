using UnityEngine;
using static ObjectPool.ObjectInfo;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    private ObjectType MyObjectType;

    [SerializeField]
    private Vector3 SpawnPosition;

    private void Update()
    {
        var bullet = ObjectPool.Instance.GetObject(MyObjectType);
        bullet.GetComponent<Bullet>().OnCreate(SpawnPosition, transform.rotation);
    }
}
