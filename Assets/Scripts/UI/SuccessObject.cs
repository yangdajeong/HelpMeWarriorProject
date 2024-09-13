using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SuccessObject : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    void Update()
    {
        transform.position = transform.position + Vector3.down * PlayerController.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer.Contain(collision.gameObject.layer))
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
