using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    private bool isMove;
    private Vector3 destination; // ������
    private Rigidbody2D rigidBody;
    //private Vector3 touchStartPos;
    //private Vector3 playerStartPos;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)  // ��ġ�� ���۵� ��
            {
                // ��ġ ���� ��ġ ���
                //touchStartPos = Camera.main.ScreenToWorldPoint(touch.position);
                //playerStartPos = transform.position;    
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)  // ��ġ ���� �� (��ġ�ϰ� ������ ��, ��ġ������ �������� ���� ��)
            {
                // Raycast�� ��ġ�� ��ġ�� 2D ������Ʈ Ž��
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                if (hit.collider != null)
                {
                    destination = hit.point;
                    isMove = true;
                }
                else
                {
                    Debug.Log("Ž���Ǵ� ������Ʈ ����");
                }
            }
        }

        Move();
    }

    void Move()
    {
        if (isMove)
        {
           //float distance = Vector3.Distance(destination, transform.position);  // ���������� �Ÿ� ���
            float distance = Vector2.Distance(new Vector2(destination.x,0), new Vector2(transform.position.x, 0)); //2D
            bool isArrived = distance <= 0.1f;  // �������� ���� ������ ���
            //Debug.Log(isArrived);

            if (isArrived)
            {
                isMove = false;
                Debug.Log($"Arrived!");
                rigidBody.velocity = Vector2.zero;  // �����ϸ� �ӵ��� 0���� �����Ͽ� ����
            }
            else
            {
                //rigidBody.velocity = new Vector2(moveSpeed * destination.x,0);  
                
                Vector3 direction = destination;  // ��ġ ���� ��ġ�� ������ ��
                if (direction.x > 0)  // ���������� �̵�
                {
                    rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
                }
                else if (direction.x < 0)  // �������� �̵�
                {
                    rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
                }

                //// �÷��̾ �������� �̵��ϴ� ���� ���
                //Vector3 direction = (destination - transform.position).normalized;
                //rigidBody.velocity = direction * moveSpeed;


                // �հ��� ��ġ�� ��������� �ӵ� ���̱� (�߰����� �ӵ� ����)
                //if (distance < 1f)
                //{
                //    rigidBody.velocity *= distance;  // �Ÿ��� ���� �ӵ��� ����
                //}
            }

        }
    }
}
