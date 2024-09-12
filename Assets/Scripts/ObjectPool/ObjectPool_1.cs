//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectPool_1 : MonoBehaviour
//{
//    [System.Serializable]
//    public class Pool
//    {
//        public string poolName;             // Ǯ�� �̸�
//        public PooledObject prefab;         // �ش� Ǯ���� ����� ������
//        public int capacity;                // Ǯ�� �ִ� �뷮
//    }

//    public static ObjectPool_1 instance;

//    [SerializeField] private List<Pool> pools; // ���� ������ Ǯ�� ����
//    private Dictionary<string, Stack<PooledObject>> poolDictionary;

//    private void Awake()
//    {
//        instance = this;
//        poolDictionary = new Dictionary<string, Stack<PooledObject>>();

//        // �� Ǯ�� �ʱ�ȭ
//        foreach (Pool pool in pools)
//        {
//            Stack<PooledObject> objectPool = new Stack<PooledObject>(pool.capacity);
//            for (int i = 0; i < pool.capacity; i++)
//            {
//                PooledObject instance = Instantiate(pool.prefab);
//                instance.gameObject.SetActive(false);
//                instance.Pool = this;
//                instance.transform.parent = transform;
//                objectPool.Push(instance);
//            }
//            poolDictionary.Add(pool.poolName, objectPool);
//        }
//    }

//    // Ư�� Ǯ���� ������Ʈ�� ������ �޼���
//    public PooledObject GetFromPool(string poolName, Vector3 position, Quaternion rotation)
//    {
//        if (poolDictionary.ContainsKey(poolName))
//        {
//            Stack<PooledObject> objectPool = poolDictionary[poolName];
//            if (objectPool.Count > 0)
//            {
//                PooledObject instance = objectPool.Pop();
//                instance.transform.position = position;
//                instance.transform.rotation = rotation;
//                instance.gameObject.SetActive(true);
//                return instance;
//            }
//            else
//            {
//                // Ǯ�� �� �� ������ ���� ����
//                foreach (Pool pool in pools)
//                {
//                    if (pool.poolName == poolName)
//                    {
//                        PooledObject newInstance = Instantiate(pool.prefab);
//                        newInstance.Pool = this;
//                        newInstance.transform.position = position;
//                        newInstance.transform.rotation = rotation;
//                        return newInstance;
//                    }
//                }
//            }
//        }

//        Debug.LogWarning($"Ǯ {poolName}��(��) ã�� �� �����ϴ�.");
//        return null;
//    }

//    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
//    public void ReturnToPool(string poolName, PooledObject instance)
//    {
//        if (poolDictionary.ContainsKey(poolName))
//        {
//            instance.gameObject.SetActive(false);
//            poolDictionary[poolName].Push(instance);
//        }
//        else
//        {
//            Destroy(instance.gameObject);
//        }
//    }
//}
