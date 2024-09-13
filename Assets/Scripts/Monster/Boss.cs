using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageble
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float distance;
    [SerializeField] SpriteRenderer[] monsterSprite;
    [SerializeField] RaycastHit2D hit;
    private bool playerFront = true;
    [Range(0, 100)]
    [SerializeField] int fireProbability = 20;

    [SerializeField] int currentHp;
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }

    [SerializeField] int maxHp = 200;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    private Spawner spawner;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    private void Start()
    {
        currentHp = maxHp;

        InvokeRepeating("IsFire", 3f, 3f);
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.down * PlayerController.speed * Time.deltaTime;

        if (transform.position.y < -viewHeight)
        {
            transform.position = Vector3.up * viewHeight;
        }
    }

    public void IsFire()
    {
        for (int i = 0; i < spawner.monsterSpawnPoints.Length; i++)
        {
            int result = Random.Range(1, 100);
            if (result < fireProbability)
            {
                ObjectPools.instance.GetPool("Fire", spawner.monsterSpawnPoints[i].position, Quaternion.identity);
            }
            playerFront = true;
        }
    }


    public void Damaged(int damage)
    {
        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = Color.red;

        currentHp -= damage;

        StartCoroutine(resetColor());
    }


    public void SetSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    // 보스 전투 종료 처리
    private void Die()
    {
        if (spawner != null)
        {
            spawner.EndBossBattle();
        }

        Destroy(gameObject);  // 보스 오브젝트 파괴
    }



    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = Color.white;

        if ((currentHp <= 0))
        {
            currentHp = 0;
            Die();
        }
    }
}

