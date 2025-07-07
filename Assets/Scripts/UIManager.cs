using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button startBtn;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // ��������� ������ UI ��� �������� ����� �����
    }

    void Start()
    {
        if (menuBtn == null)
        {
            return;
        }
        menuBtn.onClick.AddListener(OpenMenu);
        nextBtn.onClick.AddListener(NextLevelMidle);

    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelEasy()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevelMidle()
    {
        SceneManager.LoadScene(2);
    }

}
