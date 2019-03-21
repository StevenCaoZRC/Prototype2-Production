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

        m_attackOneDamage = 10.0f;
        m_attackTwoDamage = 30.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    public override void MovementAnimation(bool _walk)
    {
        base.MovementAnimation(_walk);
    }

    public override void EnemyAttack()
    {
        base.EnemyAttack();
    }
    
    public override void TakeDamage(GameObject _attackedFrom)
    {
        if (!m_isHit && m_health > 0)
        {
            base.TakeDamage(_attackedFrom);
            GetWeapon().EndAttack();
            m_enemyAnim.SetTrigger("IsHit");
            m_isHit = true;
            m_health -= GetDamage(_attackedFrom.GetComponent<SpearAttack>().GetAttackType());//_attackedFrom.GetComponent<SpearAttack>().GetDamage();
            Debug.Log("Soldier health: " + m_health);

            //var moveDirection = m_rigidBody.transform.position - _attackedFrom.transform.position;
            //m_rigidBody.AddForce(moveDirection.normalized * 500f);
            StartCoroutine(ResetHit());
            if (m_health <= 0)
            {
                Die();
            }
        }
    }

    public float GetDamage(SpearAttackType _type)
    {
        if (_type == SpearAttackType.SPECIAL)
            return 80.0f;
        else
            return 30.0f;
    }

}
