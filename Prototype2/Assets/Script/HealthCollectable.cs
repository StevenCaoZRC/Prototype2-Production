using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public Health m_Health;
    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("SPAWNED");
            if (FindObjectOfType<Health>().m_currentHealth <= FindObjectOfType<Health>().m_maxHealth)
            {
                FindObjectOfType<Health>().m_currentHealth += 20;
                Destroy(gameObject);
            }
              
        }
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        yield return null;

    }
}
