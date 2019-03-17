using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool m_isAttacking = false;
    public bool m_isCharging = false;
    public bool m_isholdingCharge = false;
    public bool m_isBlocking = false;
    private bool m_isDead = false;

    public GameObject m_spear;
    public GameObject m_shield;

    //Particles
    public GameObject m_chargeReachedParticles;
    public GameObject m_chargingParticles;

    public Animator m_playerAnimator;
    public bool m_disabledInput = false;
    Rigidbody rb;

    //Helath and Stamina
    public Health m_playerHealth;
    public Stamina m_playerStamina;
    public float m_normalAttackCost = 10.0f;
    public float m_SpecialAttackCost = 40.0f;

    //Charge timer
    float m_chargeHoldTimer = 0.0f;
    float m_chargeHoldRequired = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);
        rb = GetComponent<Rigidbody>();
        m_isDead = false;
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
    }

    public void PlayerAttack()
    {
        if(!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (GameManager.GetAxisOnce(ref m_isAttacking, "Spear") && !m_isCharging && m_playerStamina.m_currentStamina >= m_normalAttackCost)
            {
                m_playerAnimator.GetComponent<Animator>().SetTrigger("NormalAttack"); //Start attack anim
                m_spear.GetComponent<SpearAttack>().NormalAttack(); //Start attack mechanic
                m_playerStamina.m_currentStamina -= 20.0f;

            }
            else if(Input.GetAxis("ChargeSpear") > 0f && m_playerStamina.m_currentStamina >= m_SpecialAttackCost) //If player is charging
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime; //Increase charge timer
                m_chargingParticles.SetActive(true); //Activate charging particles
                if(m_playerAnimator.GetBool("ChargeSpear") == false)
                {
                    m_playerAnimator.SetBool("ChargeSpear", true);
                }

                //If X is held more than 3 seconds, charge attack ready
                if (m_chargeHoldTimer >= m_chargeHoldRequired)
                {
                    m_isholdingCharge = true;
                    m_chargeReachedParticles.SetActive(true);
                }
            }
            else
            {
                m_chargeHoldTimer = 0;
                m_isCharging = false;
                m_chargingParticles.SetActive(false);
                m_playerAnimator.SetBool("ChargeSpear", false);
            }

            //If charge is ready, use special attack
            if (Input.GetAxis("ChargeSpear") == 0f && m_isholdingCharge)
            {
                m_playerAnimator.GetComponent<Animator>().SetTrigger("SpecialAttack");
                m_playerStamina.m_currentStamina -= 60.0f;

                ChargeAttack();
            }
        }
    }

    public void PlayerBlock()
    {
        if (!m_shield.GetComponent<ShieldBlock>().GetIsBlocking() && m_playerStamina.m_currentStamina >= 20.0f)
        {
            if (GameManager.GetAxisOnce(ref m_isBlocking, "Shield"))
            {

                m_playerStamina.m_currentStamina -= 20.0f;
                m_shield.GetComponent<ShieldBlock>().PlayerBlock();
                //Debug.Log(m_shield.GetComponent<ShieldBlock>().GetIsColliding()); 
                if (m_shield.GetComponent<ShieldBlock>().CheckCol())// &&m_shield.GetComponent<ShieldBlock>().GetBlockedAttack())
                {
                    Debug.Log("PUSHED");

                    Debug.Log(m_disabledInput);
                    rb.AddForce(-transform.forward * 1000, ForceMode.Impulse);
                    m_disabledInput = true;
                    // Debug.Log(currrentVelocity);
                    Debug.Log(rb.velocity);
                }

            }
        }
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

    public bool GetIsDead()
    {
        return m_isDead;
    }
}
