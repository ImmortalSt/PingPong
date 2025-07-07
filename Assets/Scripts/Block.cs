using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int hitsToKill;      //���������� ��� �������� ���������� ������������� ������
    public int points;          //���������� ��� �������� ���������� �����, ������� ���������� ����� ��� ����������� ����� �����
    private int numberOfHits;   //���������� ��� �������� ���������� ��� �������� �� ���� ������
    

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
                GameManager.Instance.BlockDestroyed(this.gameObject); // ���������� GameManager �� ����������� �����
                Destroy(this.gameObject); // ���������� ����
            }
        }

    }

   
}
