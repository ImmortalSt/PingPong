using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int hitsToKill;      //Переменная для хранения количества выдерживаемых ударов
    public int points;          //Переменная для хранения количества очков, которые заработает игрок при уничтожении этого блока
    private int numberOfHits;   //Переменная для хранения количества уже принятых на себя ударов
    

    void Start()
    {
        numberOfHits = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            if (numberOfHits == hitsToKill)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Platform");
                player.SendMessage("addPoints", points);
                GameManager.Instance.BlockDestroyed(this.gameObject); // Уведомляем GameManager об уничтожении блока
                Destroy(this.gameObject); // Уничтожаем блок
            }
        }

    }

   
}
