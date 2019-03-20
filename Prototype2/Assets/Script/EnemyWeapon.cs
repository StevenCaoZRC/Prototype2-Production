using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    bool m_hitSomething = false;
    bool m_isAttacking = false;
    public float m_attackDamage = 10.0f;

    float m_attackEndTimer = 0.0f;
    float m_attackTotalTimer = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
    }

    void Update()
    {
        if ((m_isAttacking || m_hitSomething) && this.GetComponent<Collider>().enabled)
        {
            m_attackEndTimer += Time.deltaTime;
            if (m_attackEndTimer > m_attackTotalTimer)
            {
                EndAttack();
            }
        }
    }

    public void NormalAttack()
    {
        m_attackEndTimer = 0.0f;
        m_isAttacking = true;
        this.GetComponent<Collider>().enabled = true;

        //StartCoroutine(EnemyAttacking());
    }

    public void EndAttack()
    {
        m_attackEndTimer = 0.0f;

        this.GetComponent<Collider>().enabled = false;
        m_hitSomething = false;
        m_isAttacking = false;
        //StartCoroutine(EnemyAttacking());
    }


    //IEnumerator EnemyAttacking()
    //{
    //    m_isAttacking = true;
    //    yield return new WaitForSeconds(0.6f); //Prepare attack anim
    //    //this.GetComponent<Collider>().enabled = true;
        
    //    Debug.Log("Enemy finished attack");
    //    yield return new WaitForSeconds(0.3f);
    //    //this.GetComponent<Collider>().enabled = false;
    //    yield return new WaitForSeconds(0.3f);

    //    m_hitSomething = false;
    //    m_isAttacking = false;

    //    yield return null;
    //}

    public void SetAttacking(bool _attack)
    {
        m_isAttacking = _attack;
    }

    public bool GetAttacking()
    {
        return m_isAttacking;
    }

    public float GetAttackDamage()
    {
        return m_attackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !m_hitSomething && m_isAttacking)
        {
            m_hitSomething = true;
            other.gameObject.GetComponent<PlayerControl>().TakeDamage(gameObject);
        }
    }
}
