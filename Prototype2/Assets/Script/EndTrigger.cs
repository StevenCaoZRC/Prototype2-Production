﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameObject WinParticles;
    // Start is called before the first frame update
    void Start()
    {
        WinParticles.SetActive(false);
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
        }
    }
    IEnumerator EndLevel()
    {

        //Spawn particles
        WinParticles.SetActive(true);
        //Load menu screen
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
        WinParticles.SetActive(false);

        yield return null;
    }
}