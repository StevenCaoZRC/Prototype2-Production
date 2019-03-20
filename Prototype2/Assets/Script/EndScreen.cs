using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    public GameObject Player;
    public GameObject m_winScreen;
    public GameObject m_loseScreen;


    // Use this for initialization
    void Start()
    {
        if (Player.GetComponent<PlayerControl>().m_LevelCleared)
        {
            SetWinScreen();
        }
        else if (Player.GetComponent<PlayerControl>().m_isDead)
        {
            SetLoseScreen();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void SetWinScreen()
    {
        m_loseScreen.SetActive(false);
        m_winScreen.SetActive(true);
    }

    public void SetLoseScreen()
    {
        m_winScreen.SetActive(false);
        m_loseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
