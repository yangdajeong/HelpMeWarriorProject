//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectPool_1 : MonoBehaviour
//{
//    [System.Serializable]
//    public class Pool
//    {
//        public string poolName;             // 풀의 이름
//        public PooledObject prefab;         // 해당 풀에서 사용할 프리팹
//        public int capacity;                // 풀의 최대 용량
//    }

//    public static ObjectPool_1 instance;

//    [SerializeField] private List<Pool> pools; // 여러 종류의 풀을 관리
//    private Dictionary<string, Stack<PooledObject>> poolDictionary;

//    private void Awake()
//    {
//        instance = this;
//        poolDictionary = new Dictionary<string, Stack<PooledObject>>();

//        // 각 풀을 초기화
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

//    // 특정 풀에서 오브젝트를 꺼내는 메서드
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
//                // 풀이 다 차 있으면 새로 생성
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

//        Debug.LogWarning($"풀 {poolName}을(를) 찾을 수 없습니다.");
//        return null;
//    }

//    // 오브젝트를 풀로 반환하는 메서드
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
