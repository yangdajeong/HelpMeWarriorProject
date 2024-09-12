using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBarrior : MonoBehaviour
{
    [SerializeField] LayerMask transPauseLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transPauseLayer.Contain(collision.gameObject.layer))
        {
            PooledObject pooledObject = collision.gameObject.GetComponent<PooledObject>();
            if (pooledObject == null)
                return;

            pooledObject.Release();
        }
    }
}
