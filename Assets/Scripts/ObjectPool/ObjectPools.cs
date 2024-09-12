using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PoolSetup
{
    public string poolName;           // Ǯ�� �̸�
    public PooledObject prefab;       // Ǯ���� ������Ʈ ������
    public int capacity = 10;         // �� Ǯ�� �ִ� �뷮
}


public class ObjectPools : MonoBehaviour
{
    [SerializeField] List<PoolSetup> poolSetups;  // Ǯ ���� ����Ʈ

    private Dictionary<string, Stack<PooledObject>> poolDictionary = new Dictionary<string, Stack<PooledObject>>();

    public static ObjectPools instance;

    private void Awake()
    {
        instance = this;
        foreach (PoolSetup setup in poolSetups)
        {
            CreatePool(setup);  // �� Ǯ�� ����
        }
    }

    // Ǯ ����
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

        // Ǯ�� Dictionary�� �߰�
        poolDictionary.Add(setup.poolName, objectPool);
    }

    // Ǯ���� ������Ʈ ��������
    public PooledObject GetPool(string poolName, Vector3 position, Quaternion rotation)
    {
        // Ǯ�¾����� �ش� Ǯ�� �������� ã�� ���� �ν��Ͻ�ȭ
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

    // ������Ʈ�� Ǯ�� ��ȯ
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
