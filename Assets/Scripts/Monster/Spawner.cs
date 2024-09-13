using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform[] monsterSpawnPoints;
    [SerializeField] Navigation navigation;

    [Header("MonsterSpawnProbability")]
    [Range(0, 100)]
    public int EasySpawnProbability = 20;

    [Range(0, 100)]
    public int HardSpawnProbability = 5;

    [SerializeField] float spawnDelay = 2f;
    private float time;

    [Header("Boss")]
    [SerializeField] Slider naviSlider;
    private Queue<float> bossPointQueue = new Queue<float>();  // ���� ��ȯ ����Ʈ�� �����ϴ� ť
    [SerializeField] float[] bossPoint;
    [SerializeField] GameObject bossPrefab;
    private bool bossBattle = false;
    private bool successCheck = false;

    [Header("Sussces")]
    [SerializeField] GameObject successPrefab;


    private void Start()
    {
        for (int i = 0; i < bossPoint.Length; i++)
        {
            bossPointQueue.Enqueue(bossPoint[i]);
        }
    }

    private void Update()
    {
        if (naviSlider.value >= 1 && !successCheck)
        {
            Success();
        }


        if (bossBattle || bossPointQueue.Count <= 0)
            return;

            if (naviSlider.value >= bossPointQueue.Peek() - (10f / navigation.Destination))
            {
                SpawnBoss();
                return;
            }
        

        if (PlayerController.speed == 0 && !bossBattle)
            return;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            for (int i = 0; i < monsterSpawnPoints.Length; i++)
            {
                int result = Random.Range(1, 100);

                // �ϵ� ���� ����
                if (result < HardSpawnProbability)
                {
                    ObjectPools.instance.GetPool("MonsterHard", monsterSpawnPoints[i].position, Quaternion.identity);
                }
                // ���� ���� ����
                else if (result < EasySpawnProbability)
                {
                    ObjectPools.instance.GetPool("MonsterEasy", monsterSpawnPoints[i].position, Quaternion.identity);
                }
            }


            time = spawnDelay;
        }
    }


    private void SpawnBoss()
    {
        if (naviSlider.value >= bossPointQueue.Peek() - (8f / navigation.Destination))
        {
            Debug.Log("������ȯ");
            bossBattle = true;
            bossPointQueue.Dequeue();
            GameObject bossInstance = Instantiate(bossPrefab, monsterSpawnPoints[2].position, Quaternion.identity);
            Boss bossScript = bossInstance.GetComponent<Boss>();
            bossScript.SetSpawner(this);  // �������� Spawner�� ������ ����
        }
    }

    public void EndBossBattle()
    {
        bossBattle = false;  // ���� ���� ���� �� �ٽ� ���� ���� ����
    }

    private void Success()
    {
        successCheck = true;
        Instantiate(successPrefab, monsterSpawnPoints[2].position, Quaternion.identity);
    }
}
