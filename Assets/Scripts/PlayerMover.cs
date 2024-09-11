using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    private bool isMove;
    private Vector3 destination; // 목적지
    private Rigidbody2D rigidBody;
    private Vector3 touchStartPos;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)  // 터치가 시작될 때
            {
                // 터치 시작 위치 기록
                touchStartPos = Camera.main.ScreenToWorldPoint(touch.position); 
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)  // 터치 중일 때 (터치하고 움직일 때, 터치했지만 움직이지 않을 때)
            {
                // Raycast로 터치한 위치의 2D 오브젝트 탐지
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                if (hit.collider != null)
                {
                    destination = hit.point;
                    isMove = true;
                }
                else
                {
                    Debug.Log("탐지되는 오브젝트 없음");
                }
            }
        }

        Move();
    }

    void Move()
    {
        if (isMove)
        {
           //float distance = Vector3.Distance(destination, transform.position);  // 목적지와의 거리 계산
            float distance = Vector2.Distance(new Vector2(destination.x, destination.z), new Vector2(transform.position.x, transform.position.z)); //2D
            bool isArrived = distance <= 0.1f;  // 목적지에 거의 도달한 경우
            Debug.Log(distance);
            Debug.Log(isArrived);

            if (isArrived)
            {
                isMove = false;
                rigidBody.velocity = Vector2.zero;  // 도착하면 속도를 0으로 설정하여 멈춤
            }
            else
            {
                Vector3 direction = destination - touchStartPos;  // 터치 시작 위치와 목적지 비교
                if (direction.x > 0)  // 오른쪽으로 이동
                {
                    rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
                    Debug.Log("오른쪽으로 이동");
                }
                else if (direction.x < 0)  // 왼쪽으로 이동
                {
                    rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
                    Debug.Log("왼쪽으로 이동");
                }

                //// 플레이어가 목적지로 이동하는 방향 계산
                //Vector3 direction = (destination - transform.position).normalized;
                //rigidBody.velocity = direction * moveSpeed;


                // 손가락 위치에 가까워지면 속도 줄이기 (추가적인 속도 감속)
                //if (distance < 1f)
                //{
                //    rigidBody.velocity *= distance;  // 거리에 따라 속도를 감속
                //}
            }

        }
    }
}
