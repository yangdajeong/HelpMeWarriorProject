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
    private Queue<float> bossPointQueue = new Queue<float>();  // 보스 소환 포인트를 관리하는 큐
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

                // 하드 몬스터 스폰
                if (result < HardSpawnProbability)
                {
                    ObjectPools.instance.GetPool("MonsterHard", monsterSpawnPoints[i].position, Quaternion.identity);
                }
                // 이지 몬스터 스폰
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
            Debug.Log("보스소환");
            bossBattle = true;
            bossPointQueue.Dequeue();
            GameObject bossInstance = Instantiate(bossPrefab, monsterSpawnPoints[2].position, Quaternion.identity);
            Boss bossScript = bossInstance.GetComponent<Boss>();
            bossScript.SetSpawner(this);  // 보스에게 Spawner의 참조를 전달
        }
    }

    public void EndBossBattle()
    {
        bossBattle = false;  // 보스 전투 종료 후 다시 몬스터 스폰 가능
    }

    private void Success()
    {
        successCheck = true;
        Instantiate(successPrefab, monsterSpawnPoints[2].position, Quaternion.identity);
    }
}
