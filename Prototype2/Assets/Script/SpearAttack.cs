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
    float m_spearSpeed = 0.2f;
    bool m_hitSomething = false;
    bool m_isAttacking = false;

    //float m_normalAttackAmount = 20.0f;
    //float m_specialAttackAmount = 50.0f;

    public Transform m_startMarker;
    public Transform m_endMarker;

    SpearAttackType m_spearAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAttack()
    {
        m_spearAttack = SpearAttackType.NORMAL;
        this.GetComponent<Collider>().enabled = true;
        StartCoroutine(TempMoveSpear());
        m_isAttacking = true;
    }
    public void ChargeAttack()
    {
        m_spearAttack = SpearAttackType.SPECIAL;
        this.GetComponent<Collider>().enabled = true;
        StartCoroutine(TempMoveSpear());

        m_isAttacking = true;
    }

    public bool GetIsAttacking()
    {
        return m_isAttacking;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy" && !m_hitSomething)
    //    {
    //        m_hitSomething = true;
    //        Debug.Log("Player attacking");
    //        other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && !m_hitSomething)
        {
            m_hitSomething = true;
            Debug.Log("Player attacking");
            other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        this.GetComponent<Collider>().enabled = false;
    //        m_hitSomething = false;
    //        Debug.Log("Player finish attack");
    //    }
    //}

    IEnumerator TempMoveSpear()
    {
        //this is temporary. Will check hit with animated position.
        transform.position = Vector3.MoveTowards(transform.position, m_endMarker.position, 100.0f);
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector3.MoveTowards(transform.position, m_startMarker.position, 100.0f);
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
        m_hitSomething = false;
        Debug.Log("Player finish attack");
        yield return null;
    }

    IEnumerator TempSpecialSpear()
    {
        //this is temporary. Will check hit with animated position.
        transform.position = Vector3.MoveTowards(transform.position, m_endMarker.position, 100.0f);
        yield return new WaitForSeconds(0.2f);
        transform.position = Vector3.MoveTowards(transform.position, m_startMarker.position, 100.0f);
        yield return new WaitForSeconds(0.2f);
        transform.position = Vector3.MoveTowards(transform.position, m_endMarker.position, 100.0f);
        yield return new WaitForSeconds(0.2f);
        transform.position = Vector3.MoveTowards(transform.position, m_startMarker.position, 100.0f);
        m_spearAttack = SpearAttackType.NORMAL;
        m_isAttacking = false;
        this.GetComponent<Collider>().enabled = false;
        m_hitSomething = false;
        Debug.Log("Player finish attack");
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
