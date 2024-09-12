using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEasy : MonoBehaviour, IDamageble
{
    [SerializeField] int hp = 100;
    [SerializeField] LayerMask returnObject;
    [SerializeField] SpriteRenderer[] monsterSprite;
    
    public bool transPause;
    public bool TransPause { get { return transPause; } set { transPause = value; } }

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        if (transPause)
            return;

        transform.position = transform.position + Vector3.down * PlayerController.speed * Time.deltaTime;

        if (transform.position.y < -viewHeight)
        {
            transform.position = Vector3.up * viewHeight;
        }
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

        pooledObject.Release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(returnObject.Contain(collision.gameObject.layer))
        {
            Die();
        }
    }

    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = new (1, 1, 1);

        if ((hp <= 0))
        {
            Die();
        }
    }
}
