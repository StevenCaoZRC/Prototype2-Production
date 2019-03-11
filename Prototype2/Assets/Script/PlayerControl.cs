using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool m_isAttacking = false;
    public bool m_isCharging = false;

    public GameObject m_spear;
    public float m_chargeHoldTimer;
    public float m_chargeHoldRequired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
    }

    public void PlayerAttack()
    {
        if(!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (Input.GetAxis("Spear") > 0f && !m_isCharging)
            {
                //lerp position forward and back
                m_spear.GetComponent<SpearAttack>().NormalAttack();

            }
            else if(Input.GetAxis("ChargeSpear") > 0f)
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime;
                if (m_chargeHoldTimer >= m_chargeHoldRequired)
                {
                    m_spear.GetComponent<SpearAttack>().ChargeAttack();
                }
            }
        }
    }


}
