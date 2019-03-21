using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //Player.GetComponent<PlayerControl>().m_LevelCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(EndLevel());
            Player.GetComponent<PlayerControl>().m_LevelCleared = true;
        }
    }
    IEnumerator EndLevel()
    {

        //Load menu screen
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");

        yield return null;
    }
}
