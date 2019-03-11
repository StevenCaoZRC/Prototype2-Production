using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool m_isAttacking = false;
    public GameObject m_spear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
    }

    public void PlayerAttack()
    {
        if(Input.GetAxis("Spear") > 0f)
        {
            //lerp position forward and back
            m_spear.GetComponent<SpearAttack>().SetAttacking(true);
            
        }
        else
        {
            m_spear.GetComponent<SpearAttack>().SetAttacking(false);
        }
    }


}
