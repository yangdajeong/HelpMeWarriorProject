using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] Transform[] monsterSpawnPoints;

    [Range(0,100)]
    public int spawnProbability = 20;

    [SerializeField] float spawnDelay = 2;
    private float time;

    private void Update()
    {
        if (PlayerController.speed == 0)
        {
            //time = Time.time;
            return;
        }

        time -= Time.deltaTime;


        if (time <= 0)
        {
            for (int i = 0; i < monsterSpawnPoints.Length; i++)
            {
                int result = Random.Range(1, 100);
                if (result < spawnProbability)
                {
                    ObjectPool_1.instance.GetPool(monsterSpawnPoints[i].position, Quaternion.identity);
                }
            }

            time = spawnDelay;
        }
    }
}
