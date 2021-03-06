﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool bButtonSelected;
    private AudioSource m_Next;


    // Use this for initialization
    void Start ()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        bButtonSelected = true;

        m_Next = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !bButtonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            bButtonSelected = true;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (null != m_Next)
                m_Next.Play();
        }

        if (Input.GetMouseButtonDown(0))
            eventSystem.SetSelectedGameObject(selectedObject);
    }

    private void OnDisable()
    {
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        bButtonSelected = true;
    }
}
