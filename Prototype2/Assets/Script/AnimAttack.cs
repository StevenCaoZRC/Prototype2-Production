using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAttack : MonoBehaviour
{
    public GameObject _character;
    private Enemy _enemyScript;
    //private PlayerControl PlayerControl;

    // Start is called before the first frame update
    void Start()
    {
        if(_character.tag == "Enemy")
            _enemyScript = transform.parent.gameObject.GetComponent<Enemy>();
        //if (_character.tag == "Player")
        //    PlayerControl = transform.parent.gameObject.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAttackEvent()
    {
        _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().NormalAttack();
    }

    public void EnemyEndAttackEvent()
    {
        _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().EndAttack();
    }

    //public void EnemyAttackEvent()
    //{
    //    _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().NormalAttack();
    //}

    //public void EnemyEndAttackEvent()
    //{
    //    _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().EndAttack();
    //}
}
