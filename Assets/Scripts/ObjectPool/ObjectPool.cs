using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool_1 : MonoBehaviour
{
    [SerializeField] PooledObject prefab;
    [SerializeField] int capacity;

    private Stack<PooledObject> objectPool;

    public static ObjectPool_1 instance;

    private void Awake()
    {
        instance = this;
        CreatePool(prefab, capacity);
    }


    public void CreatePool(PooledObject prefab,  int capacity)
    {
        this.prefab = prefab;
        this.capacity = capacity;

        objectPool = new Stack<PooledObject>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            instance.transform.parent = transform;
            objectPool.Push(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count > 0)
        {
            PooledObject instance = objectPool.Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(prefab);
            instance.Pool = this;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        if (objectPool.Count < capacity)
        {
            instance.gameObject.SetActive(false);
            objectPool.Push(instance);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
}
