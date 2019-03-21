using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
  
    public int m_Area1Counter;
    public int m_Area2Counter;
    //public int m_Area2p5Counter;
    public int m_Area3Counter;
    public GameObject m_Area1;
    public GameObject m_Area2;
    //public GameObject m_Area2p5;
    public GameObject m_Area3;

    [HideInInspector]
    public int m_currentCounter;
    [HideInInspector]
    public GameObject m_currentArea;
    bool m_areaCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Area1.SetActive(true);
        m_Area2.SetActive(false);
        //m_Area2p5.SetActive(false);
        m_Area3.SetActive(false);
        m_currentArea = m_Area1;
        m_currentCounter = m_Area1Counter;
    }

    // Update is called once per frame
    void Update()
    {
        //When one area is cleared
        if (m_currentArea == m_Area1 && m_areaCleared)
        {
            m_currentArea = m_Area2;
            m_currentCounter = m_Area2Counter;
            m_areaCleared = false;
            m_currentArea.SetActive(true);
        }
        else if (m_currentArea == m_Area2 && m_areaCleared)
        {
            m_currentArea.SetActive(false);
            m_currentArea = m_Area3;

            m_currentCounter = m_Area3Counter;
            m_areaCleared = false;

            m_currentArea.SetActive(true);

        }
        else if (m_currentArea == m_Area3 && m_areaCleared)
        {
            //m_currentArea = m_Area3;
            m_areaCleared = true;
            m_currentCounter = 0;
            m_currentArea.SetActive(false);

        }

        //Level Cleared
        if (m_currentCounter == 0)
        {
            m_areaCleared = true;
            m_currentArea.SetActive(false);
        }
        else if (m_currentCounter != 0)
        {
            m_areaCleared = false;
            m_currentArea.SetActive(true);
        }
    }
}
