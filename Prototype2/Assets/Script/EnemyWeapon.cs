using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    bool m_hitSomething = false;
    bool m_isAttacking = false;
    public float m_attackDamage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
    }

    public void NormalAttack()
    {
        StartCoroutine(EnemyAttacking());
    }


    IEnumerator EnemyAttacking()
    {
        m_isAttacking = true;
        yield return new WaitForSeconds(0.2f); //Prepare attack anim
        this.GetComponent<Collider>().enabled = true;
        
        Debug.Log("Enemy finished attack");
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.3f);

        m_hitSomething = false;
        m_isAttacking = false;

        yield return null;
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
        if (other.tag == "Player" && !m_hitSomething)
        {
            Debug.Log("Enemy attacking " + m_hitSomething);

            m_hitSomething = true;
            other.gameObject.GetComponent<PlayerControl>().TakeDamage(gameObject);
        }
    }
}
