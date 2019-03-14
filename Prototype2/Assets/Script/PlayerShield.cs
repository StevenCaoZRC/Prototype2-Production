using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : Shield
{
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_shieldType = ShieldType.PLAYERSHIELD;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
       
    }

    public void PlayerBlock()
    {
        m_isBlocking = true;
        StartCoroutine(PlayerBlockAttack());
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.transform.GetChild(1).tag);
        if (m_isBlocking)
        {
            if (other.gameObject.transform.GetChild(1).tag == "GuardSword")
            {
                m_blockedAttack = true;
                m_isBlocking = false;
                m_colliding = true;
                Debug.Log("Player Blocking");
            }
            else
            {
                m_colliding = false;
                m_isBlocking = false;
                m_blockedAttack = false;
            }
        }
        else
        {
            m_colliding = false;
            m_isBlocking = false;
        }
    }

    IEnumerator PlayerBlockAttack()
    {
        Debug.Log("Attack Blocked By Player");
        m_blockedAttack = false;
        yield return null;
    }
}
