using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    private bool isMove;
    private Vector3 destination;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        { 
            Debug.Log("1");
            
            Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));

            RaycastHit hit;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                destination = pos;
                Debug.Log(pos);
                //isMove = true;
            
            
            {
                //Debug.Log("검색되는거 없음");
            }
        }
        Move();
    }

    void Move()
    {
        if(isMove) 
        {
            bool isAlived = Vector3.Distance(destination, transform.position) <= 0.1f;

            if(isAlived) 
            {
                isMove = false;
            }

            else
            {
                Vector3 direction = destination - transform.position;
                transform.forward = direction;
                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }
        }
    }
}
