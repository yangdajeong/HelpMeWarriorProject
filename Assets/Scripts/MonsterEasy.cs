using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEasy : MonoBehaviour, IDamageble
{
    [SerializeField] int hp = 100;

    public float speed;
    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -viewHeight)
        {
            transform.position = Vector3.up * viewHeight;
        }
    }
}
