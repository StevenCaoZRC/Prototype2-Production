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
    bool m_triggerEntered = false;
    bool m_playerIsInfront = false;

    public GameObject m_windStreakParticles;

    //float m_normalAttackAmount = 20.0f;
    //float m_specialAttackAmount = 50.0f;

    float m_attackEndTimer = 0.0f;
    float m_attackTotalTimer = 0.2f;

    SpearAttackType m_spearAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_playerIsInfront = false;
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
        m_windStreakParticles.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("-----------------front?: " + m_playerIsInfront);
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
        m_spearAttack = SpearAttackType.NORMAL;
        this.GetComponent<Collider>().enabled = true;
        m_windStreakParticles.SetActive(true);
        //StartCoroutine(TempMoveSpear());
        m_isAttacking = true;
    }

    public void EndAttack()
    {
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        m_hitSomething = false;
        m_attackEndTimer = 0.0f;
        //Debug.Log("Player finish attack");
        this.GetComponent<Collider>().enabled = false;
        m_windStreakParticles.SetActive(false);
        m_triggerEntered = false;
    }

    public void ChargeAttack()
    {
        m_spearAttack = SpearAttackType.SPECIAL;
        m_isAttacking = true;

        m_windStreakParticles.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
    }

    public bool GetIsAttacking()
    {
        return m_isAttacking;
    }

    public void SetAttacking(bool _attack)
    {
        m_isAttacking = _attack;
    }


    private void OnTriggerStay(Collider other)
    {
        //if collider == enemy

        if (other.tag == "Enemy" && !m_hitSomething && !m_triggerEntered)
        {

            m_triggerEntered = true;
            m_hitSomething = true;

            //Debug.Log("Guard hit");

            if(m_playerIsInfront && other.gameObject.GetComponent<Enemy>().GetWeapon().GetAttacking())
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
            }
            else if(m_playerIsInfront && !other.gameObject.GetComponent<Enemy>().GetWeapon().GetAttacking())
            {
                other.gameObject.GetComponent<EnemyGuard>().BlockPlayer(gameObject);

            }
            else if(!m_playerIsInfront)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
            }
        }
        else if (other.tag == "EnemyCollider" && !m_hitSomething && !m_triggerEntered)
        {
            m_triggerEntered = true;
            m_hitSomething = true;
        
            Debug.Log("Soldier attacked: " + other.gameObject.name);
            other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
        }

        /*
        else if (other.tag == "EnemyDefCollider" && !m_hitSomething && !m_triggerEntered)
        {
            if (other.transform.parent.gameObject.GetComponent<Enemy>().GetWeapon().GetAttacking())
            {
                m_triggerEntered = true;
                m_hitSomething = true;
                Debug.Log("P attacked enemy front");
                other.transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
            }
            else if (other.transform.parent.gameObject.GetComponent<EnemyGuard>().GetShield().GetIsBlocking())
            {
                m_triggerEntered = true;
                m_hitSomething = true;
                Debug.Log("P attacked enemy front while attacking");
                other.transform.parent.gameObject.GetComponent<EnemyGuard>().BlockPlayer(gameObject);
            }
        }
        */
        //else if (other.tag == "EnemyDefCollider" && !m_hitSomething && !m_triggerEntered)
        //{
        //    if(other.transform.parent.gameObject.GetComponent<Enemy>().GetWeapon().GetAttacking())
        //    {
        //        m_triggerEntered = true;
        //        m_hitSomething = true;
        //        Debug.Log("P attacked enemy front");
        //        other.transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
        //    }
        //    else
        //    {
        //        m_triggerEntered = true;
        //        m_hitSomething = true;
        //        Debug.Log("P attacked enemy front while attacking");
        //        other.transform.parent.gameObject.GetComponent<EnemyGuard>().BlockPlayer(gameObject);
        //    }

        //}
    }

    //IEnumerator TempMoveSpear()
    //{
    //    yield return new WaitForSeconds(0.2f); //Prepare attack anim
    //    this.GetComponent<Collider>().enabled = true;
    //    m_windStreakParticles.SetActive(true);

    //    yield return new WaitForSeconds(0.5f);
    //    m_spearAttack = SpearAttackType.NORMAL;
    //    m_isAttacking = false;
    //    m_hitSomething = false;
    //    Debug.Log("Player finish attack");
    //    this.GetComponent<Collider>().enabled = false;
    //    m_windStreakParticles.SetActive(false);

    //    yield return null;
    //}

    //IEnumerator TempSpecialSpear()
    //{
    //    m_windStreakParticles.SetActive(true);
    //    this.GetComponent<Collider>().enabled = true;

    //    yield return new WaitForSeconds(0.1f); //Prepare attack anim
    //    m_windStreakParticles.SetActive(false);

    //    //Special spear has different timing as it plays instantly 
    //    //  compared to the normal attack which plays out the full anim sequence

    //    yield return new WaitForSeconds(0.2f); //Prepare attack anim
    //    m_spearAttack = SpearAttackType.NORMAL;
    //    m_isAttacking = false;
    //    m_hitSomething = false;
    //    Debug.Log("Player finish CHARGE attack");
    //    this.GetComponent<Collider>().enabled = false;

    //    yield return null;
    //}

    public SpearAttackType GetAttackType()
    {
        return m_spearAttack;
    }

    public void SetAttackType(SpearAttackType _attackType)
    {
        m_spearAttack = _attackType;
    }

    public void SetPlayerInFront(bool _infront)
    {
        m_playerIsInfront = _infront;
    }
}
