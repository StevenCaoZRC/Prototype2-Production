using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldBlock : MonoBehaviour
{
    bool m_blockedAttack = false;
    bool m_IsBlocking = false;
    bool m_colliding = false;
    public Rigidbody Player;
   
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
       // rb.AddExplosionForce(1000, this.transform.position, 500.0f, 3.0f);
    }

    public bool GetIsBlocking()
    {
        return m_IsBlocking;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (m_IsBlocking)
        {
            if (other.name == "Mace" )
            {
                m_blockedAttack = true;
                m_IsBlocking = false;
                m_colliding = true;
                //Player.velocity = Vector3.zero;
                Debug.Log("Player Blocking");
                

            }
            else if (other.tag == "Spear" )
            {
                m_blockedAttack = true;
                //m_IsBlocking = true;
                m_IsBlocking = false;
                m_colliding = true;
                // rb.AddForce(new Vector3(1000, 0, 0));
                Debug.Log("Enemy Blocking");
            }
            else {
                m_colliding = false;
                m_IsBlocking = false;
            }
        }
    }
    public bool CheckCol()
    {
        return m_colliding;
    }
  
    public bool GetBlockedAttack()
    {
        return m_blockedAttack;
    }
    
    IEnumerator PlayerBlockAttack()
    {
        //yield return new WaitForSeconds(2.0f);
       
        yield return new WaitForSeconds(0.5f);
    
        //this.GetComponent<Collider>().enabled = false;
       
        Debug.Log("Played Block an Attack");
        m_blockedAttack = false;
        //m_IsBlocking = false;

        //m_colliding = false;
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
