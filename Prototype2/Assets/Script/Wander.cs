using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    private float m_movementSpeed = 30.0f;
    private float m_startWaitTime = 3.0f; //Wander time before they switch directions
    private float m_waitTime = 10.0f; //Wander time before they switch directions

    private Transform m_initPosition;
    public Transform m_destination;

    NavMeshAgent m_navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
         m_navMeshAgent = this.GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        if(m_destination != null)
        {
            Vector3 target = m_destination.transform.position;
            m_navMeshAgent.SetDestination(target);
            m_navMeshAgent.stoppingDistance = 10.0f;
        }
    }
}
