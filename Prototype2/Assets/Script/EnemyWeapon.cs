using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    bool m_isAttacking = false;
    public float m_attackDamage = 20.0f;

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
        if (other.tag == "Player")
        {
            Debug.Log("Player attacking");
            other.gameObject.GetComponent<PlayerControl>().TakeDamage(gameObject);
        }
    }
}
