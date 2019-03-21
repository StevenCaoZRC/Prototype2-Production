using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldType
{
    ENEMYSHIELD,
    PLAYERSHIELD
}
public class Shield : MonoBehaviour
{
    protected bool m_blockedAttack = false;
    protected bool m_isBlocking = false;
    protected bool m_colliding = false;
    public bool m_knockedBack = false;
    protected ShieldType m_shieldType;

    protected float m_blockEndTimer = 0.0f;
    protected float m_blockTotalTimer = 0.75f;

    public GameObject m_shieldHitParticles;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_isBlocking = false;
        m_blockedAttack = false;
        m_colliding = false;
        m_blockEndTimer = 0.0f;
        if (m_shieldHitParticles != null)
        {
            m_shieldHitParticles.SetActive(false);
        }
    }

    public virtual void Update()
    {
        
    }

    public virtual void Block()
    {
        m_isBlocking = true;
    }

    public void EndBlock()
    {
        m_isBlocking = false;
        m_blockedAttack = false;
        m_colliding = false;
        m_blockEndTimer = 0.0f;
        if (m_shieldHitParticles != null)
        {
            m_shieldHitParticles.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {

    }
    public ShieldType GetShieldType()
    {
        return m_shieldType;
    }

    public bool GetIsColliding()
    {
        return m_colliding;
    }

    public bool GetIsBlocking()
    {
        return m_isBlocking;
    }

    public bool GetBlockedAttack()
    {
        return m_blockedAttack;
    }

    public bool CheckCol()
    {
        return m_colliding;
    }

    public void SetCol(bool _colliding)
    {
        m_colliding = _colliding;
    }

    public void PlayParticles(bool _on)
    {
        if(m_shieldHitParticles != null)
        {
            m_shieldHitParticles.SetActive(_on);
        }
    }
}
