using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] float speed;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int fireDamage = 20;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    private void Start()
    {
        rigid.AddForce(transform.up * -1 * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contain(collision.gameObject.layer))
        {
            IDamageble damageble = collision.transform.GetComponent<IDamageble>();  
            if (damageble != null)
            {
                damageble.Damaged(fireDamage);

                PooledObject pooledObject = gameObject.GetComponent<PooledObject>();
                if (pooledObject == null)
                {
                    Destroy(this);
                }

                pooledObject.Release();
            }
        }
    }
}
