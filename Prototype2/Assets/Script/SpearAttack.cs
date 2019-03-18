using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpearAttackType
{
    NORMAL,
    SPECIAL
}

public class SpearAttack : MonoBehaviour
{
    //float m_spearSpeed = 0.2f;
    bool m_hitSomething = false;
    bool m_isAttacking = false;
    public GameObject m_windStreakParticles;

    //float m_normalAttackAmount = 20.0f;
    //float m_specialAttackAmount = 50.0f;

    SpearAttackType m_spearAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
        m_windStreakParticles.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAttack()
    {
        m_spearAttack = SpearAttackType.NORMAL;
        StartCoroutine(TempMoveSpear());
        m_isAttacking = true;
    }
    public void ChargeAttack(Animator _playerAnim)
    {
        m_spearAttack = SpearAttackType.SPECIAL;
        StartCoroutine(TempSpecialSpear());
        m_isAttacking = true;
    }

    public bool GetIsAttacking()
    {
        return m_isAttacking;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EnemyCollider" && !m_hitSomething)
        {
            m_hitSomething = true;
            Debug.Log("Player attacking");
            other.transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
        }
    }

    IEnumerator TempMoveSpear()
    {
        yield return new WaitForSeconds(0.2f); //Prepare attack anim
        this.GetComponent<Collider>().enabled = true;
        m_windStreakParticles.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        m_hitSomething = false;
        Debug.Log("Player finish attack");
        this.GetComponent<Collider>().enabled = false;
        m_windStreakParticles.SetActive(false);

        yield return null;
    }

    IEnumerator TempSpecialSpear()
    {
        m_windStreakParticles.SetActive(true);
        this.GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(0.1f); //Prepare attack anim
        m_windStreakParticles.SetActive(false);

        //Special spear has different timing as it plays instantly 
        //  compared to the normal attack which plays out the full anim sequence

        yield return new WaitForSeconds(0.2f); //Prepare attack anim
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        m_hitSomething = false;
        Debug.Log("Player finish CHARGE attack");
        this.GetComponent<Collider>().enabled = false;

        yield return null;
    }

    public SpearAttackType GetAttackType()
    {
        return m_spearAttack;
    }

    public void SetAttackType(SpearAttackType _attackType)
    {
        m_spearAttack = _attackType;
    }
}
