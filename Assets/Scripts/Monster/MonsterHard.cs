using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHard : MonoBehaviour, IDamageble
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float distance;
    [SerializeField] SpriteRenderer[] monsterSprite;
    [SerializeField] RaycastHit2D hit;
    //[SerializeField] int collisionDamage;
    private bool playerFront = true;

    [SerializeField] int currentHp;
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }

    [SerializeField] int maxHp = 200;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    //[SerializeField] TextMeshProUGUI damagedText;
    [SerializeField] GameObject hpBar;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        hpBar.SetActive(false);
    }

    private void Start()
    {
        currentHp = maxHp;
    }

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
    //    Debug.DrawRay(transform.position, Vector2.down, Color.red, attackDistance); //레이 보이게 하기
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
        if(!hpBar.activeSelf)
            hpBar.SetActive(true);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = Color.red;

        currentHp -= damage;

        StartCoroutine(resetColor());
    }

    private void Die()
    {
        PooledObject pooledObject = GetComponent<PooledObject>();
        if (pooledObject == null)
            return;

        hpBar.SetActive(false);
        currentHp = maxHp;
        transform.position = new Vector3(0, 20, 0);
        pooledObject.Release();
    }



    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = new Color(0.2793699f, 0.4995889f, 0.8113208f);

        if ((currentHp <= 0))
        {
            currentHp = 0;  
            Die();
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (playerLayer.Contain(collision.gameObject.layer))
    //    {
    //        IDamageble damageble = collision.transform.GetComponent<IDamageble>();
    //        if (damageble != null)
    //        {
    //            damageble.Damaged(collisionDamage);
    //        }
    //    }
    //}
}

