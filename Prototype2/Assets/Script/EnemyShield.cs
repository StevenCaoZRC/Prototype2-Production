﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : Shield
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_shieldType = ShieldType.ENEMYSHIELD;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {

    }

    //public void EnemyBlock()
    //{
    //    StartCoroutine(EnemyBlockAttack());
    //}

    //IEnumerator EnemyBlockAttack()
    //{
    //    m_isBlocking = true;
    //    Debug.Log("Enemy Block an Attack");
    //    yield return new WaitForSeconds(0.5f);
    //    m_blockedAttack = false;
    //    m_isBlocking = false;
    //    yield return null;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("?????????");
    //    if (m_isBlocking)
    //    {
    //        if (other.tag == "PlayerSpear" && !m_blockedAttack)
    //        {
    //            m_blockedAttack = true;
    //            m_isBlocking = false;
    //            m_colliding = true;
    //            Debug.Log("Enemy Blocking");
    //        }
    //        else
    //        {
    //            m_colliding = false;
    //            m_isBlocking = false;
    //            m_blockedAttack = false;
    //        }
    //    }
    //}
    
}
