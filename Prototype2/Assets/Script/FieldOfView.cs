using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float m_viewRadius;
    [Range(0, 360)]
    public float m_viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public GameObject m_target;

    bool m_targetWithinRadius = false;
    bool m_targetWithinFOV = false;


    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.3f));
    }

    IEnumerator FindTargetsWithDelay(float _delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            FindPlayerTarget();
        }
    }

    //finds all targets and draws line gizmo if within view
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            //Does target fall into view angle
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < m_viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

    }

    void FindPlayerTarget()
    {
        m_target = null;
        m_targetWithinFOV = false;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_viewRadius, targetMask);
        //for (int i = 0; i < targetsInViewRadius.Length; i++)
        //{
        //    GameObject target = targetsInViewRadius[i].gameObject;    
        if(targetsInViewRadius.Length > 0)
        {
            if (targetsInViewRadius[0].tag == "Player")
            {
                m_targetWithinRadius = true;
                //Get the direction towards the target from the enemy position
                Vector3 dirToTarget = (targetsInViewRadius[0].transform.position - transform.position).normalized;
                
                //Checking if the enemy is within the view angle
                if (Vector3.Angle(transform.forward, dirToTarget) < m_viewAngle / 2)
                {
                    //Calculate the distance towards the target
                    float distToTarget = Vector3.Distance(transform.position, targetsInViewRadius[0].transform.position);

                    //Raycast from enemy to target while ignoring obstacles
                    if (Physics.Raycast(transform.position, dirToTarget, distToTarget, targetMask))
                    {
                        m_targetWithinFOV = true;
                        m_target = targetsInViewRadius[0].gameObject;
                    }
                    else
                    {
                        m_targetWithinFOV = false;
                    }
                }
            }
            else
            {
                Debug.Log("There is something else on the player layer in the scene?");
            }
        }
        else
        {
            m_targetWithinRadius = false;
            m_targetWithinFOV = false;

        }
    }

    public Vector3 DirectionFromAngle(float _angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            _angleInDegrees+= transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(_angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(_angleInDegrees*Mathf.Deg2Rad));
    }

    public GameObject GetTarget()
    {
        return m_target;
    }

    public bool GetIsTargetWithinRadius()
    {
        return m_targetWithinRadius;
    }

    public bool GetIsTargetWithinFOV()
    {
        return m_targetWithinFOV;
    }
}
