using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int attackPower;
    [SerializeField] int attackDelay;

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
        else if (hit.collider == null)
        {

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
        speed = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = 1;
    }
}
