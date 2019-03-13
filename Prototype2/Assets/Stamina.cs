using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stamina : MonoBehaviour
{
    [HideInInspector]
    public float m_maxStamina;
    [HideInInspector]
    public float m_currentStamina;
    public Image m_staminaOrb;

    public float m_rechargeStamina;
    // Start is called before the first frame update
    void Start()
    {
        m_maxStamina = 100.0f;
        m_currentStamina = m_maxStamina;
        m_rechargeStamina = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_staminaOrb.fillAmount = m_currentStamina / m_maxStamina;
        if (m_currentStamina < m_maxStamina)
        {
            m_currentStamina += Time.deltaTime * m_rechargeStamina;
            Debug.Log(m_currentStamina);
        }
    }
}
