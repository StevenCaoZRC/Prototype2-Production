using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool m_isAttacking = false;
    public bool m_isCharging = false;
    public bool m_isholdingCharge = false;
    public bool m_isBlocking = false;
    public bool m_pushedBack = false;


    public GameObject m_spear;
    public GameObject m_shield;
    public GameObject m_chargeReachedParticles;
    public GameObject m_chargingParticles;
    public Animator m_playerAnimator;
    public bool m_disabledInput = false;
    public Rigidbody rb;
    public Health m_playerHealth;
    public Stamina m_playerStamina;

    float m_chargeHoldTimer = 0.0f;
    float m_chargeHoldRequired = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
        PlayerBlock();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_playerHealth.m_currentHealth -= 10.0f;
        }
        else if (Input.GetKeyDown(KeyCode.X) && m_playerHealth.m_currentHealth < m_playerHealth.m_maxHealth)
        {
            m_playerHealth.m_currentHealth += 10.0f;
        }
        if (m_playerHealth.m_currentHealth > m_playerHealth.m_maxHealth)
        {
            m_playerHealth.m_currentHealth = m_playerHealth.m_maxHealth;
        }

        if (m_pushedBack)
        {
            m_shield.GetComponent<ShieldBlock>().SetCol(false);
        }
    }

    public void PlayerAttack()
    {
        if(!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (GameManager.GetAxisOnce(ref m_isAttacking, "Spear") && !m_isCharging)
            {
                //lerp position forward and back
                m_spear.GetComponent<SpearAttack>().NormalAttack(m_playerAnimator);

            }
            else if(Input.GetAxis("ChargeSpear") > 0f)
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime;
                m_chargingParticles.SetActive(true);
                Debug.Log("uh oh");
                if(m_playerAnimator.GetBool("ChargeSpear") == false)
                {
                    m_playerAnimator.SetBool("ChargeSpear", true);
                }
                //If X is held more than 3 seconds, charge attack ready
                if (m_chargeHoldTimer >= m_chargeHoldRequired)
                {
                    m_isholdingCharge = true;
                    m_chargeReachedParticles.SetActive(true);
                    //m_playerAnimator.SetBool("IsCharging", true);

                }
            }
            else
            {
                m_chargeHoldTimer = 0;
                m_isCharging = false;
                m_chargingParticles.SetActive(false);
                m_playerAnimator.SetBool("ChargeSpear", false);
                //Debug.Log("Let go");
            }

            //If charge is ready, use special attack
            if (Input.GetAxis("ChargeSpear") == 0f && m_isholdingCharge)
            {
                ChargeAttack();
            }
        }
    }

    public void PlayerBlock()
    {
        
        if (GameManager.GetAxisOnce(ref m_isBlocking, "Shield"))
        {
            if (m_playerStamina.m_currentStamina >= 20.0f)
            { m_playerStamina.m_currentStamina -= 20.0f;     }
            if (!m_shield.GetComponent<ShieldBlock>().GetIsBlocking() && m_playerStamina.m_currentStamina >= 20.0f)
            {
                

                m_shield.GetComponent<ShieldBlock>().PlayerBlock();
                //Debug.Log(m_shield.GetComponent<ShieldBlock>().GetIsColliding()); 
                if (m_shield.GetComponent<ShieldBlock>().CheckCol())// &&m_shield.GetComponent<ShieldBlock>().GetBlockedAttack())
                {
                    Debug.Log("PUSHED");
                    StartCoroutine(PushBackPlayer());

                }
                else
                {
                    m_pushedBack = false;
                }

            }
        }
      
    }
    

    IEnumerator PushBackPlayer()
    {
        //wait for animation 

        rb.velocity = Vector3.zero;
        rb.AddForce(-transform.forward.normalized * 500.0f, ForceMode.Impulse);
        m_pushedBack = true;

        yield return new WaitForSeconds(0.5f);
        yield return null;
    }
    void ChargeAttack()
    {
        m_spear.GetComponent<SpearAttack>().ChargeAttack(m_playerAnimator);
        Debug.Log("suPERSAIYAN");

        //Reset timer values
        m_chargeHoldTimer = 0;
        m_isCharging = false;
        m_isholdingCharge = false;
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);

    }
}
