using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    float m_spearSpeed = 0.2f;
    bool m_hitSomething = false;
    bool m_isAttacking = false;

    public Transform m_startMarker;
    public Transform m_endMarker;

    // Start is called before the first frame update
    void Start()
    {
        m_isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAttacking)
        {
            this.GetComponent<Collider>().enabled = true;
            StartCoroutine(TempMoveSpear());
            m_isAttacking = false;
        }
    }

    public void SetAttacking(bool _attacking)
    {
        m_isAttacking = _attacking;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && !m_hitSomething)
        {
            m_hitSomething = true;
            Debug.Log("Player attacking");
            other.gameObject.GetComponent<Enemy>().TakeDamage(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            this.GetComponent<Collider>().enabled = false;
            m_hitSomething = false;
            Debug.Log("Player finish attack");
            //other.gameObject.GetComponent<Enemy>().Die();
        }
    }

    IEnumerator TempMoveSpear()
    {
        //this is temporary. Will check hit with animated position.
        transform.position = Vector3.MoveTowards(transform.position, m_endMarker.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector3.MoveTowards(transform.position, m_startMarker.position, 0.5f);
        yield return null;
    }
}
