using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : Shield
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        m_shieldType = ShieldType.PLAYERSHIELD;
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("blocking?: " + m_isBlocking + " colliding: " + m_colliding);


    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
       
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("acttivated collision ?");

        // Debug.Log(other.gameObject.transform.GetChild(1).tag);
        if (m_isBlocking)
        {
            Debug.Log("other layer : " + other.gameObject.layer);
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyWeapon"))
            {
                m_blockedAttack = true;
                //m_isBlocking = false;
                m_colliding = true;
                Debug.Log("Player Blocking");
            }
        }
    }

    //IEnumerator PlayerBlockAttack()
    //{
    //    Debug.Log("Attack Blocked By Player");
    //    m_blockedAttack = false;
    //    m_isBlocking = false;
    //    yield return null;
    //}
}
