using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] Transform[] monsterSpawnPoints;

    [Range(0,100)]
    public int EasySpawnProbability = 20;

    [Range(0, 100)]
    public int HardSpawnProbability = 5;

    [SerializeField] float spawnDelay = 2;
    private float time;

    private void Update()
    {
        if (PlayerController.speed == 0)
            return;
        

        time -= Time.deltaTime;


        if (time <= 0)
        {
            for (int i = 0; i < monsterSpawnPoints.Length; i++)
            {
                int result = Random.Range(1, 100);

                if (result < HardSpawnProbability) // HardMonster
                {
                    ObjectPools.instance.GetPool("MonsterHard", monsterSpawnPoints[i].position, Quaternion.identity);
                }
                else if (result < EasySpawnProbability) // EasyMonster
                {
                    ObjectPools.instance.GetPool("MonsterEasy",monsterSpawnPoints[i].position, Quaternion.identity);
                }
            }

            time = spawnDelay;
        }
    }
}
