using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 300f;
    private Rigidbody2D body;
    private bool canMove = true;

    private int playerLives;
    private int playerPoints;  

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        playerLives = 3;
        playerPoints = 0;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float dx = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
            body.velocity = new Vector2(dx, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void StartMoving()
    {
        canMove = true;
    }

    void addPoints(int points)
    {
        GameManager.Instance.AddPoints(points);
    }



}
