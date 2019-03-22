using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    GUARD,
    SOLDIER
}

public class Enemy : MonoBehaviour
{
  
    protected Transform m_target;
    protected EnemyType m_enemyType;
    public Animator m_enemyAnim;
    public GameObject m_enemyWeapon;
    public GameObject m_hitParticle;
    public EnemyShield m_enemyShield;
    public GameObject m_heathCollectable;
    public Transform m_enemyPosition;
    public AreaScript currentArea;

    protected bool m_isHit = false;
    protected bool m_isDead = false;

    protected float m_health = 100.0f;
    protected float m_attackOneDamage = 10.0f;
    protected float m_attackTwoDamage = 30.0f;
    protected float m_takeDamageAmount = 20.0f; //Should be taking from player not self

    //Wander code
    protected bool m_inCombat = false;


    // Start is called before the first frame update
    public virtual void Start()
    {
        m_health = 100.0f;
        m_isHit = false;
        m_isDead = false;
        if (m_hitParticle != null)
        {
            m_hitParticle.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Destroy enemy when death animation finishes and enemy is dead
        if (m_enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Death")
            && m_enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && m_isDead)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.2f);
        if (m_hitParticle != null)
        {
            m_hitParticle.SetActive(false);
        }
        if(GetShield() != null)
        {
            GetShield().Block();
        }
        m_isHit = false;
    }

    public EnemyType GetEnemyType()
    {
        return m_enemyType;
    }

    public bool GetIsDead()
    {
        return m_isDead;
    }

    public bool GetIsHit()
    {
        return m_isHit;
    }

    protected IEnumerator DeathAnimation()
    {
        m_isDead = true;
        if (m_enemyAnim != null)
        {
            m_enemyAnim.SetTrigger("IsDead");
        }
        yield return new WaitForSeconds(2.0f);
        Destroy(transform.parent.gameObject);
        yield return null;
    }

    public virtual void MovementAnimation(bool _walk)
    {
        if(m_enemyAnim != null)
        {
            m_enemyAnim.SetBool("Walk", _walk);
        }
        else
        {
            Debug.Log("Non-existing Animator");
        }
    }

    public void Patrol()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    public virtual void EnemyAttack()
    {
        if(m_enemyWeapon != null && !m_isDead && !m_isHit)
        {
            if (!m_enemyWeapon.GetComponent<EnemyWeapon>().GetAttacking())
            {
                m_enemyWeapon.GetComponent<EnemyWeapon>().SetAttacking(true);
                m_enemyAnim.SetTrigger("Attack");
            }
        }
    }

    public virtual void TakeDamage(GameObject _attackedFrom)
    {
        if (m_hitParticle != null)
        {
            FindObjectOfType<AudioManager>().PlayOnce("SwordSwing");

            m_hitParticle.SetActive(true);
        }

    }

    protected void Die()
    {
        if (m_enemyAnim != null)
        {
            if (m_hitParticle != null)
            {
                m_hitParticle.SetActive(false);
            }
              
            FindObjectOfType<AreaScript>().m_currentCounter -= 1;

            m_enemyAnim.SetTrigger("IsDead");
            m_isDead = true;
            Instantiate(m_heathCollectable, new Vector3(m_enemyPosition.position.x, m_enemyPosition.position.y + 2.5f, m_enemyPosition.position.z), m_enemyPosition.rotation);
        }
    }

    public virtual void EnemyBlock() { }

    public EnemyWeapon GetWeapon()
    {
        return m_enemyWeapon.GetComponent<EnemyWeapon>();
    }

    public EnemyShield GetShield()
    {
        if(m_enemyShield != null)
        {
            return m_enemyShield.GetComponent<EnemyShield>();
        }
        else
        {
            return null;
        }
    }
}
