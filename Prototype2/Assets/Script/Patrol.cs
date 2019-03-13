using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public bool m_patrolWaiting = true; //Stop on each node?
    public float m_totalWaitTime = 3.0f;
    float m_directionProb = 0.2f;
    public List<WayPoint> m_patrolPoints;

    NavMeshAgent m_navMeshAgent;
    int m_currentPoint;
    bool m_travelling;
    bool m_waiting;
    bool m_patrolForward;
    float m_waitTimer;

    //[SerializeField]
    public bool m_targetingPlayer = false;
    Transform m_playerTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (m_navMeshAgent == null)
        {
            Debug.Log("Error: NavMeshAgent Disappeared");
        }
        else
        {
            if(m_patrolPoints != null && m_patrolPoints.Count >= 2)
            {
                m_currentPoint = 0;
                SetDestination();
            }
            else
            {
                Debug.Log(name + " needs more patrol points");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_travelling && m_navMeshAgent.remainingDistance <= 2.0f)
        {
            m_travelling = false;
            if (!m_targetingPlayer)
            {
                //If we're going to wait, then wait
                if (m_patrolWaiting)
                {
                    m_waiting = true;
                    m_waitTimer = 0.0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
            else
            {
                //If targeting player

            }
            
        }

        if(m_waiting)
        {
            m_waitTimer += Time.deltaTime;
            if (m_waitTimer >= m_totalWaitTime)
            {
                m_waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0.0f, 1.0f) <= m_directionProb)
        {
            m_patrolForward = !m_patrolForward;
        }

        if(m_patrolForward)
        {
            m_currentPoint = (m_currentPoint + 1) % m_patrolPoints.Count;
        }
        else
        {
            --m_currentPoint;
            if (m_currentPoint < 0)
            {
                m_currentPoint = m_patrolPoints.Count - 1;
            }
        }
    }

    private void SetDestination()
    {
        if(!m_targetingPlayer)
        {
            if (m_patrolPoints != null)
            {
                Vector3 target = m_patrolPoints[m_currentPoint].transform.position;
                m_navMeshAgent.SetDestination(target);
                m_travelling = true;
            }
        }
        else
        {
            if(m_playerTarget != null)
            {
                Vector3 target = m_playerTarget.position;
                m_navMeshAgent.SetDestination(target);
                m_travelling = true;
            }
        }
    }
}
