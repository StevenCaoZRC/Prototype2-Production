using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    bool m_hitSomething = false;
    bool m_isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAttacking)
        {
            this.GetComponent<Collider>().enabled = true;
        }
        else
        {
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void SetAttacking(bool _attacking)
    {
        m_isAttacking = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("reach?");
        if (other.tag == "Enemy")
        {
            m_hitSomething = true;
            Debug.Log("Player attacking");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_hitSomething = false;
            m_isAttacking = false;

            Debug.Log("Player finish attack");
        }
    }
}
