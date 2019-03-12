using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool m_isAttacking = false;
    public bool m_isBlocking = false;
    public bool m_isCharging = false;
    public bool m_isholdingCharge = false;

    public GameObject m_spear;
    public GameObject m_shield;
    public GameObject m_chargeReachedParticles;
    public GameObject m_chargingParticles;
    Rigidbody rb;
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
    void FixedUpdate()
    {
        PlayerAttack();

        PlayerBlock();
       
    }

    public void PlayerAttack()
    {
        if (!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (GameManager.GetAxisOnce(ref m_isAttacking, "Spear") && !m_isCharging)
            {
                //lerp position forward and back
                m_spear.GetComponent<SpearAttack>().NormalAttack();

            }
            else if (Input.GetAxis("ChargeSpear") > 0f)
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime;
                m_chargingParticles.SetActive(true);

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
        if (!m_shield.GetComponent<ShieldBlock>().GetIsBlocking())
        {
            if (GameManager.GetAxisOnce(ref m_isBlocking, "Shield"))
            {
               
                m_shield.GetComponent<ShieldBlock>().PlayerBlock();
            //Debug.Log(m_shield.GetComponent<ShieldBlock>().GetIsColliding()); 
                if (m_shield.GetComponent<ShieldBlock>().CheckCol())// &&m_shield.GetComponent<ShieldBlock>().GetBlockedAttack())
                {
                    Debug.Log("PUSHED");
                   // rb.velocity = Vector3.zero;
                    rb.AddForce(-transform.forward * 100, ForceMode.Impulse);
                    
                }
             }
        }
           
        
    }


    void ChargeAttack()
    {
        m_spear.GetComponent<SpearAttack>().ChargeAttack();
        Debug.Log("suPERSAIYAN");

        //Reset timer values
        m_chargeHoldTimer = 0;
        m_isCharging = false;
        m_isholdingCharge = false;
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);

    }
}
