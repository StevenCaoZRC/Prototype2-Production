using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldBlock : MonoBehaviour
{
    bool m_blockedAttack = false;
    bool m_IsBlocking = false;
    bool m_colliding = false;


    // Start is called before the first frame update
    void Start()
    {
        m_IsBlocking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public bool GetIsColliding()
    {
        return m_colliding;
    }

    public void PlayerBlock()
    {
        m_IsBlocking = true;
        StartCoroutine(PlayerBlockAttack());
    }


    public void EnemyBlock()
    {
        m_IsBlocking = true;
        StartCoroutine(EnemyBlockAttack());
       
    }

    public bool GetIsBlocking()
    {
        return m_IsBlocking;
    }

   private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.transform.GetChild(1).tag);
        if (m_IsBlocking)
        {
            if (other.gameObject.transform.GetChild(1).tag == "GuardSword" )
            {
                m_blockedAttack = true;
                m_IsBlocking = false;
                m_colliding = true;
                Debug.Log("Player Blocking");
            }
            else
            {
                m_colliding = false;
                m_IsBlocking = false;
                m_blockedAttack = false;
            }
        }
        else
        {
            m_colliding = false;
            m_IsBlocking = false;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.tag);
    //    if (m_IsBlocking)
    //    {
    //        if (other.tag == "Spear" && !m_blockedAttack)
    //        {
    //            m_blockedAttack = true;
    //            m_IsBlocking = false;
    //            m_colliding = true;
    //            Debug.Log("Enemy Blocking");
    //        }
    //        else
    //        {
    //            m_colliding = false;
    //            m_IsBlocking = false;
    //            m_blockedAttack = false;
    //        }
    //    }
    //}

    public bool CheckCol()
    {
        return m_colliding;
    }
    public void SetCol(bool _colliding)
    {
        m_colliding = _colliding;
    }
    public bool GetBlockedAttack()
    {
        return m_blockedAttack;
    }
    
    IEnumerator PlayerBlockAttack()
    {
        //yield return new WaitForSeconds(2.0f);
        //yield return new WaitForSeconds(0.5f);
        Debug.Log("Played Block an Attack");
        m_blockedAttack = false;
        //yield return new WaitForSeconds(1.0f);
        //m_IsBlocking = false;
     
        yield return null;
    }

    IEnumerator EnemyBlockAttack()
    {
        yield return new WaitForSeconds(0.5f);
        //play animation
        m_blockedAttack = false;
        Debug.Log("Enemy Block an Attack");
        yield return null;
    }

   
}
