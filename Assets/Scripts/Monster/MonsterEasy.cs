using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterEasy : MonoBehaviour, IDamageble
{
    [SerializeField] LayerMask returnObject;
    [SerializeField] SpriteRenderer[] monsterSprite;
    //[SerializeField] int collisionDamage;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] int currentHp;
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }

    [SerializeField] int maxHp = 200;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    //[SerializeField] TextMeshProUGUI damagedText;
    [SerializeField] GameObject hpBar;

    float viewHeight;

    private void Awake()
    {
        //damagedText.enabled = false;
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


    public void Damaged(int damage)
    {
        if (!hpBar.activeSelf)
            hpBar.SetActive(true);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = Color.red;

        currentHp -= damage;
         //ObjectPools.instance.GetPool("DamagedText", transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        //damagedText.enabled = true;
        //damagedText.text = damage.ToString();

        StartCoroutine(resetColor());
    }

    private void Die()
    {
        PooledObject pooledObject = GetComponent<PooledObject>();
        if (pooledObject == null)
            return;

        //damagedText.enabled = false;
        hpBar.SetActive(false);
        currentHp = maxHp;
        transform.position = new Vector3(0, 20, 0);
        pooledObject.Release();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(returnObject.Contain(collision.gameObject.layer))
    //    {
    //        Die();
    //    }
    //}

    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < monsterSprite.Length; i++)
            monsterSprite[i].color = new (1, 1, 1);

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
