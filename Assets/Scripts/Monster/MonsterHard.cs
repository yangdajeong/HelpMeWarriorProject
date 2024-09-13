using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHard : MonoBehaviour, IDamageble
{
    [SerializeField] int hp = 100;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float distance;
    [SerializeField] SpriteRenderer[] monsterSprite;
    [SerializeField] RaycastHit2D hit;
    private bool playerFront = true;


    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    //private void Start()
    //{
    //    StartCoroutine(IsFire());
    //    //InvokeRepeating("IsFire", 2f, 3f);
    //}

    private void Update()
    {
        transform.position = transform.position + Vector3.down * PlayerController.speed * Time.deltaTime;

        if (transform.position.y < -viewHeight)
        {
            transform.position = Vector3.up * viewHeight;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Debug.DrawRay(transform.position, Vector2.down, Color.red, distance); //레이 보이게 하기
    //}

    void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, distance, playerLayer);
        if (hit.collider != null && playerFront)
        {
            StartCoroutine(IsFire());
        }
    }

    IEnumerator IsFire()
    {
        playerFront = false;
        yield return new WaitForSeconds(3f);
        ObjectPools.instance.GetPool("Fire", transform.position, Quaternion.identity);
        playerFront = true;
    }


    public void Damaged(int damage)
    {
        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = Color.red;

        hp -= damage;

        StartCoroutine(resetColor());
    }

    private void Die()
    {
        PooledObject pooledObject = GetComponent<PooledObject>();
        if (pooledObject == null)
            return;

        transform.position = new Vector3(0, 20, 0);
        pooledObject.Release();
    }



    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = new Color(0.2793699f, 0.4995889f, 0.8113208f);

        if ((hp <= 0))
        {
            Die();
        }
    }
}

