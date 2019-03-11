using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool m_isAttacking = false;
    public bool m_isHit = false;
    public bool m_isAlive = false;

    public float m_health;

    Transform m_target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Movement()
    {
        
    }

    public virtual void Attack()
    {

    }

    public virtual void TakeDamage()
    {

    }

    public virtual void Die()
    {
        Destroy(this);
    }

    
}
