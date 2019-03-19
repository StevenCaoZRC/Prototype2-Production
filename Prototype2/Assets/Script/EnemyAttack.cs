using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Enemy _enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        _enemyScript = transform.parent.gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackEvent()
    {
        _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().NormalAttack();
    }

    public void EndAttackEvent()
    {
        _enemyScript.GetWeapon().GetComponent<EnemyWeapon>().EndAttack();
    }
}
