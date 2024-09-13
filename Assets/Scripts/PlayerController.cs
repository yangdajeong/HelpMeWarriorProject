using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageble
{
    [SerializeField] int hp;
    [SerializeField] int attackPower;
    [SerializeField] int attackDelay;

    [SerializeField] SpriteRenderer[] playerSprite;
    [SerializeField] float distance;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] LayerMask transPauseLayer;
    RaycastHit2D hit;

    public static int speed = 1;

    private Animator animator;

    private bool isAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.up, Color.red, distance); //레이 보이게 하기
    }

    void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up, distance, monsterLayer);
        if (hit.collider != null && !isAttack)
        {
            AttackAni();
        }
    }

    public async Task AttackAni() 
    {
        isAttack = true;
        animator.SetTrigger("isAttack");

        await Task.Delay(attackDelay * 1000);

        isAttack = false;
    }

    public void Attack()
    {
        IDamageble damageble = hit.collider.GetComponent<IDamageble>();
        if (damageble != null)
        { 
            damageble.Damaged(attackPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(transPauseLayer.Contain(collision.gameObject.layer) && hit)
        {
            speed = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transPauseLayer.Contain(collision.gameObject.layer))
        {
            speed = 1;
        }
    }

    public void Damaged(int damage)
    {
        Debug.Log("공격당함");
        for (int i = 0; i < playerSprite.Length; i++)
            playerSprite[i].color = Color.red;

        hp -= damage;

        StartCoroutine(resetColor());
    }

    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < playerSprite.Length; i++)
            playerSprite[i].color =  Color.white;

        if ((hp <= 0))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
