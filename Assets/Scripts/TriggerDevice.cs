using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDevice : MonoBehaviour
{
    [SerializeField] private Transform platform; // —сылка на платформу
    [SerializeField] private float offsetY = 4f; // —мещение по оси Y, чтобы м€ч находилс€ над платформой


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D bodyBall = collision.GetComponent<Rigidbody2D>();
            if (bodyBall != null && platform != null)
            {
                // ќстанавливаем м€ч
                bodyBall.velocity = Vector2.zero;
                bodyBall.angularVelocity = 0f;

                // ѕеремещаем м€ч на платформу
                Vector2 newPosition = new Vector2(platform.position.x, platform.position.y + offsetY);
                bodyBall.transform.position = newPosition;
                GameManager.Instance.DecreaseLives();
            }
        }
    }

    
}
