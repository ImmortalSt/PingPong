using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;
    public GameObject playerObject;
    private Rigidbody2D rigidbody2D;
    public float bounceSpeedMultiplier = 1.1f;
    

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        ballInitialForce = new Vector2(100.0f, 300.0f);
        
        ballIsActive = false;
        
        ballPosition = transform.position;
    }

    void GameStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ballIsActive)
            {
                rigidbody2D.AddForce(ballInitialForce);

                ballIsActive = true;
                rigidbody2D.isKinematic = false;

                if (playerObject != null)
                {
                    Platform platform = playerObject.GetComponent<Platform>();
                    if (platform != null)
                    {
                        platform.StartMoving();
                    }
                }

            }
        }
        
    }


    void Update()
    {
        if (GameManager.Instance.HP > 0)
        {
            GameStart();
        }

        if (!ballIsActive && playerObject != null)
        {
            ballPosition.x = playerObject.transform.position.x;
            transform.position = ballPosition;
        }

        if (ballIsActive && transform.position.y < -6)
        {
            GameManager.Instance.DecreaseLives();

            ballIsActive = !ballIsActive;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -5.49f;
            transform.position = ballPosition;

            rigidbody2D.isKinematic = true;
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            rigidbody2D.velocity *= bounceSpeedMultiplier;
            Debug.Log("Collision with: " + collision.gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            ballIsActive = false;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = -5.49f;
            transform.position = ballPosition;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.isKinematic = true;
        }
    }

    
}
