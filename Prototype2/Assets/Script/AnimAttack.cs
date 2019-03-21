using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAttack : MonoBehaviour
{
    public GameObject _character;
    private Enemy m_enemyScript;
    private PlayerControl m_playerControl;
   

    // Start is called before the first frame update
    void Start()
    {
        if(_character.tag == "Enemy")
            m_enemyScript = _character.GetComponent<Enemy>();
        if (_character.tag == "EnemyCollider")
            m_enemyScript = _character.GetComponent<Enemy>();
        if (_character.tag == "Player")
            m_playerControl = _character.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyAttackEvent()
    {
        if(m_enemyScript.GetShield() != null)
        {
            m_enemyScript.GetShield().EndBlock();
        }
        if(m_enemyScript.GetWeapon() != null)
            m_enemyScript.GetWeapon().NormalAttack();
    }

    public void PNormalAttack()
    {
        m_playerControl.GetWeapon().GetComponent<SpearAttack>().NormalAttack();
    }

    public void PStartBlock()
    {
        m_playerControl.GetShield().GetComponent<PlayerShield>().Block();
    }

    public void PChargeAttack()
    {
        m_playerControl.GetWeapon().GetComponent<SpearAttack>().ChargeAttack();
    }
}
