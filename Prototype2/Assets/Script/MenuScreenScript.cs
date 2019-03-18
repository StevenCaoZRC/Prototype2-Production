using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScreenScript : MonoBehaviour {

    public GameObject m_menuScreen;
    public GameObject m_controlsScreen;
    public GameObject m_creditsScreen;

    // Use this for initialization
    void Start ()
    {
        SetMenuScreen();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetMenuScreen()
    {
        m_creditsScreen.SetActive(false);
        m_controlsScreen.SetActive(false);

        m_menuScreen.SetActive(true);
    }

    public void SetControlsScreen()
    {
        m_creditsScreen.SetActive(false);
        m_menuScreen.SetActive(false);

        m_controlsScreen.SetActive(true);
    }

    public void SetCreditsScreen()
    {
        m_menuScreen.SetActive(false);
        m_controlsScreen.SetActive(false);

        m_creditsScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
