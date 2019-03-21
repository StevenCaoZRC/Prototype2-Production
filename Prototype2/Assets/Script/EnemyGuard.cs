using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : Enemy
{
    private Rigidbody m_rigidBody;
    public Rigidbody m_playerRigidBody;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_enemyType = EnemyType.GUARD;
        m_rigidBody = GetComponent<Rigidbody>();
       
        m_attackOneDamage = 20.0f;
        m_attackTwoDamage = 50.0f;
        GetShield().Block();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        EnemyBlock();

        //if(GetShield()!= null)
        //    Debug.Log("Blocking: " + GetShield().GetIsBlocking());
    }
    public void FixedUpdate()
    {
    }

    public override void MovementAnimation(bool _walk)
    {
        base.MovementAnimation(_walk);
    }

    public override void EnemyAttack()
    {
        base.EnemyAttack();
        FindObjectOfType<AudioManager>().PlayOnce("SwordSwing");

        GetShield().EndBlock();
    }
    
    IEnumerator PushBack()
    {
        //wait for animation 
       yield return new WaitForSeconds(0.35f);

        m_playerRigidBody.AddForce(transform.forward.normalized * 250.0f, ForceMode.Impulse);
        m_enemyShield.GetComponent<EnemyShield>().m_knockedBack = false;
        m_playerRigidBody.velocity = Vector3.zero;
        yield return null;
    }

    public void BlockPlayer(GameObject _attackedFrom)
    {
        if(!m_isDead)
        {
            m_enemyShield.GetComponent<EnemyShield>().Block();
            m_enemyShield.GetComponent<EnemyShield>().PlayParticles(true);

            Debug.Log("PUSHED via block");
            StartCoroutine(PushBack());
            StartCoroutine(ResetHit());
        }
           
    }

    public override void TakeDamage(GameObject _attackedFrom)
    {
        if(!m_isHit && !m_isDead && m_health > 0)
        {
            base.TakeDamage(_attackedFrom);
            //if (!FindObjectOfType<AudioManager>().IsPlaying("MonsterHurt"))
            //    FindObjectOfType<AudioManager>().PlayOnce("MonsterHurt");
            GetWeapon().EndAttack();

            m_enemyAnim.SetTrigger("IsHit");
            //m_enemyAnim.
            m_isHit = true;
            //m_health -= GetDamage(_attackedFrom.GetComponent<SpearAttack>().GetAttackType());//_attackedFrom.GetComponent<SpearAttack>().GetDamage();
            Debug.Log("Guard health: " + m_health);

            var moveDirection = m_rigidBody.transform.position - _attackedFrom.transform.position;

            m_rigidBody.AddForce(moveDirection.normalized * 500f);
            StartCoroutine(ResetHit());

            if (m_health <= 0)
            {
                Die();
            }

            m_rigidBody.AddForce(moveDirection.normalized * 20f);

        }
    }

    public float GetDamage(SpearAttackType _type)
    {
        if (_type == SpearAttackType.SPECIAL)
            return 100.0f;
        else
            return 50.0f;
    }
}
