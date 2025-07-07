using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int HP = 3;
    public int points = 0;
    public TMP_Text livesText;
    public TMP_Text pointsText;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;       // ������ ���������� ������
    public GameObject ball;                     // ������ �� ������ ����
    public GameObject platform;                 // ������ �� ������ ���������

    public List<GameObject> blocks = new List<GameObject>(); // ������ ���� ������ � ����
    
    private static int savedPoints = 0;         // ��� �������� ���������� ��������� �����
    private static int savedHP = 0;             // ��� �������� ���������� ������

    
    
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);      // ��������� ������ GameManager ��� �������� ����� �����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //FindUIElements();                       // ������� UI �������� � �����
        UpdateUI();
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        blocks.AddRange(GameObject.FindGameObjectsWithTag("Block")); // ������� ��� ����� �� ���� � ��������� �� � ������

        // ��������������� ��������� ����� �������� �����
        if (savedHP != 0 || savedPoints != 0)
        {
            HP = savedHP;
            points = savedPoints;
            UpdateUI();
            savedHP = 0;
            savedPoints = 0;
        }
    }

    void FindUIElements()
    {
        if (livesText == null)
        {
            livesText = GameObject.Find("LivesText").GetComponent<TMP_Text>();
        }
        if (pointsText == null)
        {
            pointsText = GameObject.Find("PointsText").GetComponent<TMP_Text>();
        }
    }

    public void DecreaseLives()
    {
        HP--;
        UpdateUI();
        Debug.Log("Lives decreased. Remaining lives: " + HP);

        if (HP <= 0)
        {            
            GameOver();
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = "HP: " + HP;
            Debug.Log("HP updated to: " + HP); // ��������� ���������� ���������
        }
        else
        {
            Debug.LogError("livesText is not assigned in the inspector!");
        }

        if (pointsText != null)
        {
            pointsText.text = "Points: " + points;
        }
        else
        {
            Debug.LogError("pointsText is not assigned in the inspector!");
        }
    }


    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over. Game Over Panel activated.");
    }

    public void RestartGame()
    {
        HP = 3;
        points = 0;

        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);

        // ��������� UI
        UpdateUI();

        // ������� ����������� �����
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.LevelEasy();
        }
        else
        {
            Debug.LogError("UIManager not found!");
        }

        // ������������� ������� �����
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }

    public void RestartGameMidle()
    {
        UpdateUI();
        gameOverPanel.SetActive(false);

        // ������� ����������� �����
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.NextLevelMidle();
    }

    public void BlockDestroyed(GameObject block)
    {
        blocks.Remove(block); // ������� ������������ ���� �� ������
        if (blocks.Count == 0)
        {
            Platform platform = FindObjectOfType<Platform>();
            platform.StopMoving(); // ������������� ���������

            // ������������� ��� � ���������� ��� �� ���������
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector2.zero;
                ballRigidbody.angularVelocity = 0f;
                ball.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + 1f, ball.transform.position.z);
            }

            // ������� �� ��������� �������
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        levelCompletePanel.SetActive(true); // ���������� ������ ���������� ������
        Debug.Log("Level Complete. Level Complete Panel activated.");
    }

    public void NextLevel()
    {
        // ������� ����� � ������ �� ��������� �������
        savedPoints = points;
        savedHP = HP;
        UpdateUI();

        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.NextLevelMidle();
        }
        else
        {
            Debug.LogError("UIManager not found!");
        }

    }


}