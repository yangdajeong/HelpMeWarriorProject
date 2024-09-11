using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject monsterEasyPrefab;
    public GameObject monsterHardPrefab;

    GameObject[] monsterEasy;
    GameObject[] monsterHard;

    private void Awake()
    {
        monsterEasy = new GameObject[10];
        monsterHard = new GameObject[10];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < monsterEasy.Length; i++)
        {
            monsterEasy[i] = Instantiate(monsterEasyPrefab);
            monsterEasy[i].SetActive(false);
        }

        for (int i = 0; i < monsterHard.Length; i++)
        {
            monsterHard[i] = Instantiate(monsterHardPrefab);
            monsterHard[i].SetActive(false);
        }
    }

    //public GameObject MakeObj(string type)
    //{
    //    switch (type) 
    //    {
    //        case "MonsterEasy":
    //            for (int i = 0; i < monsterEasy.Length; i++)
    //            {
    //                if (!monsterEasy[i].activeSelf)
    //                {
    //                    monsterEasy[i].SetActive(true);
    //                    return monsterEasy[i];
    //                }
    //            }
    //    }
    //}
}
