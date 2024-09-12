using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PoolSetup
{
    public string poolName;           // 풀의 이름
    public PooledObject prefab;       // 풀링할 오브젝트 프리팹
    public int capacity = 10;         // 각 풀의 최대 용량
}


public class ObjectPools : MonoBehaviour
{
    [SerializeField] List<PoolSetup> poolSetups;  // 풀 설정 리스트

    private Dictionary<string, Stack<PooledObject>> poolDictionary = new Dictionary<string, Stack<PooledObject>>();

    public static ObjectPools instance;

    private void Awake()
    {
        instance = this;
        foreach (PoolSetup setup in poolSetups)
        {
            CreatePool(setup);  // 각 풀을 생성
        }
    }

    // 풀 생성
    public void CreatePool(PoolSetup setup)
    {
        Stack<PooledObject> objectPool = new Stack<PooledObject>(setup.capacity);
        for (int i = 0; i < setup.capacity; i++)
        {
            PooledObject instance = Instantiate(setup.prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            instance.transform.parent = transform;
            objectPool.Push(instance);
        }

        // 풀을 Dictionary에 추가
        poolDictionary.Add(setup.poolName, objectPool);
    }

    // 풀에서 오브젝트 가져오기
    public PooledObject GetPool(string poolName, Vector3 position, Quaternion rotation)
    {
        // 풀셋업에서 해당 풀의 프리팹을 찾아 새로 인스턴스화
        PoolSetup setup = poolSetups.Find(x => x.poolName == poolName);

        if (poolDictionary.ContainsKey(poolName) && poolDictionary[poolName].Count > 0)
        {
            PooledObject instance = poolDictionary[poolName].Pop();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(setup.prefab, position, rotation);
            instance.Pool = this;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
    }

    // 오브젝트를 풀로 반환
    public void ReturnPool(PooledObject instance)
    {
        string poolName = instance.gameObject.name.Replace("(Clone)", "").Trim();
        if (poolDictionary.ContainsKey(poolName) && poolDictionary[poolName].Count < poolSetups.Find(x => x.poolName == poolName).capacity)
        {
            instance.gameObject.SetActive(false);
            poolDictionary[poolName].Push(instance);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
}
