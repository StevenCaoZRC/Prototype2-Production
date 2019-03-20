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
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    protected IEnumerator ResetHit()
    {

        yield return new WaitForSeconds(0.5f);
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
                m_enemyAnim.SetTrigger("Attack");
                m_enemyWeapon.GetComponent<EnemyWeapon>().NormalAttack();
            }
        }
    }

    public virtual void TakeDamage(GameObject _attackedFrom) {}

    public virtual void Die(){}

    public virtual void EnemyBlock() { }

}
