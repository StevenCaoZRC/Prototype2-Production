﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : Enemy
{
    private Rigidbody m_rigidBody;
    public Rigidbody m_playerRigidBody;
    public GameObject m_enemyShield;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_enemyType = EnemyType.GUARD;
        m_rigidBody = GetComponent<Rigidbody>();
       
        m_attackOneDamage = 20.0f;
        m_attackTwoDamage = 50.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
      
    }
    public void FixedUpdate()
    {
        EnemyBlocking();
    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Attack()
    {
        if(!m_isAttacking)
        {

        }
    }
    public void EnemyBlocking()
    {
        if(!m_enemyShield.GetComponent<EnemyShield>().GetIsBlocking())
        {
            m_enemyShield.GetComponent<EnemyShield>().EnemyBlock();
            if (m_enemyShield.GetComponent<EnemyShield>().CheckCol())
            {
                
                Debug.Log("Enemy Pushing Player");
                StartCoroutine(PushBack());

            }
        }
      //  
        
    }

    IEnumerator PushBack()
    {
        //wait for animation 
       yield return new WaitForSeconds(0.35f);

        m_playerRigidBody.AddForce(transform.forward.normalized * 10f, ForceMode.Impulse);
        m_enemyShield.GetComponent<EnemyShield>().m_knockedBack = false;
        m_playerRigidBody.velocity = Vector3.zero;
        yield return null;
    }

    public override void TakeDamage(GameObject _attackedFrom)
    {
        if(!m_isHit)
        {
            m_isHit = true;
            m_health -= GetDamage(_attackedFrom.GetComponent<SpearAttack>().GetAttackType());//_attackedFrom.GetComponent<SpearAttack>().GetDamage();
            Debug.Log("Guard health: " + m_health);
            m_isHit = false;

            var moveDirection = m_rigidBody.transform.position - _attackedFrom.transform.position;
            m_rigidBody.AddForce(moveDirection.normalized * 20f);
        }
    }
    
    public override void Die()
    {
        //Play dead anim
        //Spawn particles
        Destroy(gameObject);
    }

    public float GetDamage(SpearAttackType _type)
    {
        if (_type == SpearAttackType.SPECIAL)
            return 50.0f;
        else
            return 20.0f;
    }
}
