using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : Enemy
{
    Rigidbody m_rigidBody;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_enemyType = EnemyType.SOLDIER;
        m_rigidBody = GetComponent<Rigidbody>();

        m_attackOneDamage = 20.0f;
        m_attackTwoDamage = 50.0f;
        m_takeDamageAmount = 20.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    public override void Movement()
    {

    }

    public override void Attack()
    {
        if (!m_isAttacking)
        {

        }
    }

    public override void TakeDamage(GameObject _attackedFrom)
    {
        if (!m_isHit)
        {
            m_isHit = true;
            m_health -= m_takeDamageAmount;
            Debug.Log("Soldier health: " + m_health);

            m_isHit = false;
        }
    }

    public override void Die()
    {
        //Play dead anim
        //Spawn particles
        Destroy(gameObject);
    }

}
