using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [HideInInspector]
    public float m_maxHealth;
    [HideInInspector]
    public float m_currentHealth;
    public Image m_healthOrb;
    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = 100.0f;
        m_currentHealth = m_maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_healthOrb.fillAmount = m_currentHealth / m_maxHealth;
    }
}
