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
    public Rigidbody rb;
    public Transform m_startPos;
    public Transform m_endPos;
    public GameObject m_whoIsHoldingShield;
    ShieldType m_shieldType;
    // Start is called before the first frame update
    void Start()
    {
        m_IsBlocking = false;
        rb = GetComponent<Rigidbody>();
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
        
    }

    public void PlayerBlock()
    {
        m_shieldType = ShieldType.PLAYERSHIELD;
        gameObject.GetComponent<Collider>().enabled = true;
        StartCoroutine(PlayerBlockAttack());
        m_IsBlocking = true;
    }


    public void EnemyBlock()
    {
        m_shieldType = ShieldType.ENEMYSHIELD;
        gameObject.GetComponent<Collider>().enabled = true;
        m_IsBlocking = true;
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
            rb.AddExplosionForce(10, gameObject.transform.position, 5.0f, 3.0f);
            Debug.Log("Player Blocking");
        }
        else if (collision.gameObject.tag == "Player" && !m_blockedAttack)
        {
            m_blockedAttack = true;
            Debug.Log("Enemy Blocking");
        }
    }

    IEnumerator PlayerBlockAttack()
    {
        m_IsBlocking = false;
        this.GetComponent<Collider>().enabled = false;
        m_blockedAttack = false;
        Debug.Log("Played Block an Attack");
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
