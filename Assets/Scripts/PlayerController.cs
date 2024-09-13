using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageble
{
    [SerializeField] int currentPlayerHp;
    public int CurrentPlayerHp { get { return currentPlayerHp; } set { currentPlayerHp = value; } }

    [SerializeField] int maxPlayerHp = 300;
    public int MaxPlayerHp { get { return maxPlayerHp; } set { maxPlayerHp = value; } }
    [SerializeField] int attackPower;
    [SerializeField] int attackDelay;
    [SerializeField] int damagedDelay;
    [SerializeField] int collisionDamage;

    [SerializeField] float attackDistance;
    [SerializeField] float frontMonsterHitDistance;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] LayerMask transPauseLayer;
    RaycastHit2D attackHit;
    RaycastHit2D frontMonsterHit;

    [SerializeField] SpriteRenderer[] playerSprite;

    public static int speed = 1;

    private Animator animator;

    private bool isAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentPlayerHp = maxPlayerHp;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.up, Color.red, 0.1f); //레이 보이게 하기
    }

    void FixedUpdate()
    {
        frontMonsterHit = Physics2D.Raycast(transform.position, Vector2.up, frontMonsterHitDistance, monsterLayer);

        attackHit = Physics2D.Raycast(transform.position, Vector2.up, attackDistance, monsterLayer);
        if (attackHit.collider != null && !isAttack)
        {
            AttackAni();
        }
    }

    private void Update()
    {
        damagedDelayTimer -= Time.deltaTime;
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
        IDamageble damageble = attackHit.collider.GetComponent<IDamageble>();
        if (damageble != null)
        {
            damageble.Damaged(attackPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transPauseLayer.Contain(collision.gameObject.layer) && attackHit)
        {
            animator.SetBool("isRun", false);
            speed = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (monsterLayer.Contain(collision.gameObject.layer) && frontMonsterHit)
        {
            Damaged(collisionDamage);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transPauseLayer.Contain(collision.gameObject.layer))
        {
            animator.SetBool("isRun", true);
            speed = 1;
        }
    }

    private float damagedDelayTimer;

    public void Damaged(int damage)
    {
        if (damagedDelayTimer > 0)
            return;

        damagedDelayTimer = damagedDelay;
        for (int i = 0; i < playerSprite.Length; i++)
            playerSprite[i].color = Color.red;

        currentPlayerHp -= damage;

        StartCoroutine(resetColor());
    }

    IEnumerator resetColor()
    {
        if ((currentPlayerHp <= 0))
        {
            currentPlayerHp = 0;
            Die();
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < playerSprite.Length; i++)
            playerSprite[i].color = Color.white;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
