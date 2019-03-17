using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public bool m_patrolWaiting = true;     //Stop on each node?
    public float m_totalWaitTime = 3.0f;    //Amount of time the enemy should stay on each spot
    public float m_distFromPlayer = 3.0f;    //Distance from player to stop

    public List<WayPoint> m_patrolPoints;   //All patrol points

    private FieldOfView m_fov;              //field of view class which detects whether the player is within the enemies current fov 
    private NavMeshAgent m_navMeshAgent;    //NavMeshAgent
    private int m_currentPoint;             //current target point
    private bool m_travelling;              //Check for whether player is in the middle of moving or not.
    private bool m_waiting;                 //Check if player is currently in the middle of waiting
    private bool m_patrolForward;           //Check for whether the player is patrolling forwards or backwards (waypoint order)
    private float m_waitTimer;              //Current time waited if waiting
    private float m_directionProb = 0.2f;   //Probability of going back or forwards

    //[SerializeField]
    public bool m_targetingPlayer = false;
    Transform m_targetPos = null;

    // Start is called before the first frame update
    void Start()
    {

        //m_distFromPlayer = 3.0f;
        m_fov = this.gameObject.GetComponent<FieldOfView>();
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
        SpotPlayer();
        if (m_navMeshAgent != null)
        {
            if (!m_targetingPlayer)
            {
                if (m_travelling && m_navMeshAgent.remainingDistance <= 2.0f)
                {
                    m_travelling = false;

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

                if (m_waiting)
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
            else
            {
                if (m_fov.GetTarget() == null && m_targetingPlayer
                    && !m_fov.GetIsTargetWithinRadius() && !m_fov.GetIsTargetWithinFOV())
                {
                    m_targetingPlayer = false;
                    m_currentPoint = UnityEngine.Random.Range(0, m_patrolPoints.Count - 1);
                    m_targetPos = m_patrolPoints[m_currentPoint].transform;
                }
                SetDestination();
            }
        }
        else
        {
            Debug.Log("You don't exist????");
        }
        FacePosition(m_targetPos.position);

    }

    private void LateUpdate()
    {


        
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

                m_navMeshAgent.stoppingDistance = 0.0f;
                m_navMeshAgent.autoBraking = true;
                m_targetPos = m_patrolPoints[m_currentPoint].transform;
                m_travelling = true;
            }
        }
        else
        {
            m_navMeshAgent.stoppingDistance = m_distFromPlayer;
            m_navMeshAgent.autoBraking = false;


            m_travelling = true;
        }
        m_navMeshAgent.SetDestination(m_targetPos.position);
    }

    //Location to make the player face
    void FacePosition(Vector3 _pos)
    {
        Vector3 dir = (_pos - transform.position).normalized;
        dir.y = 0.0f; //Dont rotate y axis

        if (dir != Vector3.zero || _pos == transform.position)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * m_navMeshAgent.speed);
        }
    }

    private void SpotPlayer()
    {
        if(m_fov != null)
        {
            //if(m_fov.GetTarget()!=null)
            //    Debug.Log("GetTarget: " + m_fov.GetTarget().name);
            //Debug.Log("m_targetingPlayer: " + m_targetingPlayer);
            //Debug.Log("IsTargetWithinFOV: " + m_fov.GetIsTargetWithinFOV());
            //Debug.Log("IsTargetWithinRadius: " + m_fov.GetIsTargetWithinRadius());

           
            if (m_fov.GetTarget() != null && !m_targetingPlayer && m_fov.GetIsTargetWithinFOV())
            {
                if(!m_fov.GetTarget().GetComponent<PlayerControl>().GetIsDead())
                {
                    m_targetPos = m_fov.GetTarget().transform;
                    m_waitTimer = 0.0f;
                    m_targetingPlayer = true;
                }
            }
            else if (m_fov.GetTarget() != null && m_targetingPlayer && m_fov.GetIsTargetWithinRadius())
            {
                if (!m_fov.GetTarget().GetComponent<PlayerControl>().GetIsDead())
                {
                    //Dont change target
                    //m_playerTarget = m_fov.GetTarget().transform;
                    m_waitTimer = 0.0f;
                    m_targetingPlayer = true;
                }
            }
        }
    }
}
