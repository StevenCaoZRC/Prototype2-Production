using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        GUARD,
        SOLDIER
    }


    protected bool m_isAttacking = false;
    protected bool m_isHit = false;
    protected Transform m_target;
    protected EnemyType m_enemyType;
    protected float m_health = 100.0f;
    protected float m_takeDamageAmount = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public EnemyType GetEnemyType()
    {
        return m_enemyType;
    }

    public virtual void Movement(){}

    public virtual void Attack(){}

    public virtual void TakeDamage(GameObject _attackedFrom) {}

    public virtual void Die(){}

    
}
