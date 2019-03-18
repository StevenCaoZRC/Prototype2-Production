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

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_isBlocking = false;
        m_blockedAttack = false;
        m_colliding = false;

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
}
