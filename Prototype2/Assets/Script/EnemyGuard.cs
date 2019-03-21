using System.Collections;
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
        //EnemyBlocking();
    }

    public override void MovementAnimation(bool _walk)
    {
        base.MovementAnimation(_walk);
    }

    public override void EnemyAttack()
    {
        base.EnemyAttack();

    }

    public override void EnemyBlock()
    {
        if(!m_enemyShield.GetComponent<EnemyShield>().GetIsBlocking())
        {
            m_enemyShield.GetComponent<EnemyShield>().EnemyBlock();
            if (m_enemyShield.GetComponent<EnemyShield>().CheckCol())
            {
                m_enemyAnim.SetTrigger("Block");
                Debug.Log("Enemy Pushing Player");
                StartCoroutine(PushBack());
            }
        }
    }

    IEnumerator PushBack()
    {
        //wait for animation 
       yield return new WaitForSeconds(0.35f);

        m_playerRigidBody.AddForce(transform.forward.normalized * 10.0f, ForceMode.Impulse);
        m_enemyShield.GetComponent<EnemyShield>().m_knockedBack = false;
        m_playerRigidBody.velocity = Vector3.zero;
        yield return null;
    }

    public override void TakeDamage(GameObject _attackedFrom)
    {
        if(!m_isHit && m_health > 0)
        {
            base.TakeDamage(_attackedFrom);

            GetWeapon().GetComponent<EnemyWeapon>().EndAttack();
            m_enemyAnim.SetTrigger("IsHit");

            m_isHit = true;
            m_health -= GetDamage(_attackedFrom.GetComponent<SpearAttack>().GetAttackType());//_attackedFrom.GetComponent<SpearAttack>().GetDamage();
            Debug.Log("Guard health: " + m_health);

            //var moveDirection = m_rigidBody.transform.position - _attackedFrom.transform.position;

            //m_rigidBody.AddForce(moveDirection.normalized * 500f);
            StartCoroutine(ResetHit());

            if (m_health <= 0)
            {
                Die();
            }

            //m_rigidBody.AddForce(moveDirection.normalized * 20f);

        }
    }

    public float GetDamage(SpearAttackType _type)
    {
        if (_type == SpearAttackType.SPECIAL)
            return 50.0f;
        else
            return 20.0f;
    }
}
