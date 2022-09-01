using System;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPool.ObjectInfo;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<ObjectType, Pool> Pools;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            TYPE_1,
            TYPE_2,
            TYPE_3,
            TYPE_4,
        }

        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField]
    private List<ObjectInfo> objectInfo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitPool();
    }

    private void InitPool()
    {
        Pools = new Dictionary<ObjectType, Pool>();

        var emptyGo = new GameObject();

        foreach (var obj in objectInfo)
        {
            var container = Instantiate(emptyGo, transform, false);
            container.name = obj.Type.ToString();

            Pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type, container.transform);
                Pools[obj.Type].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGo);
    }


    private GameObject InstantiateObject(ObjectType type, Transform parent)
    {
        var go = Instantiate(objectInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectType type)
    {
        var obj = Pools[type].Objects.Count > 0 ? Pools[type].Objects.Dequeue() : InstantiateObject(type, Pools[type].Container);

        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        Pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }

}
