using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldType
{
    ENEMYSHIELD,
    PLAYERSHIELD
}
public class ShieldBlock : MonoBehaviour
{
    bool m_blockedAttack = false;
    bool m_IsBlocking = false;
    bool m_knockBack = false;

    public GameObject m_whoIsHoldingShield;
    ShieldType m_shieldType;
    // Start is called before the first frame update
    void Start()
    {
        m_IsBlocking = false;
        if (m_whoIsHoldingShield.tag == "Enemy")
        {
            m_shieldType = ShieldType.ENEMYSHIELD;
        }
        else if (m_whoIsHoldingShield.tag == "Player")
        {
            m_shieldType = ShieldType.PLAYERSHIELD;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBlockAttack() == null)
        {
            m_IsBlocking = false;
        }
    }

    public void PlayerBlock()
    {
        m_IsBlocking = true;
        m_shieldType = ShieldType.PLAYERSHIELD;
        
        StartCoroutine(PlayerBlockAttack());
        
    }


    public void EnemyBlock()
    {
        m_shieldType = ShieldType.ENEMYSHIELD;
       
        m_IsBlocking = true;
        StartCoroutine(EnemyBlockAttack());
       // rb.AddExplosionForce(1000, this.transform.position, 500.0f, 3.0f);
    }

    public bool GetIsBlocking()
    {
        return m_IsBlocking;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !m_blockedAttack)
        {
            m_blockedAttack = true;

            Debug.Log("Player Blocking");
        }
        else if (collision.gameObject.tag == "Player" && !m_blockedAttack)
        {
            m_blockedAttack = true;

            // rb.AddForce(new Vector3(1000, 0, 0));
            Debug.Log("Enemy Blocking");
        }
      
      
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            m_IsBlocking = false;
        }
    }
    public void setBlocking(bool _isBlocking)
    {
        m_IsBlocking = _isBlocking;
    }
    public bool GetBlockedAttack()
    {
        return m_blockedAttack;
    }
    IEnumerator PlayerBlockAttack()
    {
        yield return new WaitForSeconds(0.5f);
       
        //this.GetComponent<Collider>().enabled = false;
        
        Debug.Log("Played Block an Attack");
        m_blockedAttack = false;
        //m_IsBlocking = false;
        yield return null;
    }
   

    IEnumerator EnemyBlockAttack()
    {
        m_knockBack = true;
        yield return new WaitForSeconds(0.5f);
        m_IsBlocking = false;
        //play animation
        m_blockedAttack = false;
        Debug.Log("Enemy Block an Attack");
        m_knockBack = false;
        yield return null;
    }

    public ShieldType GetShieldType()
    {
        return m_shieldType;
    }

    public void SetBlockType(ShieldType _shieldType)
    {
        m_shieldType = _shieldType;
    }
}
