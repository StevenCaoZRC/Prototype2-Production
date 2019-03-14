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
    GameObject m_target;

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.5f));

    }


    IEnumerator FindTargetsWithDelay(float _delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            FindPlayerTarget();
        }
    }

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
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, m_viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            GameObject target = targetsInViewRadius[i].gameObject;

            if (target.tag == "Player")//change to player
            {
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < m_viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.transform.position);
                    if (Physics.Raycast(transform.position, dirToTarget, distToTarget, targetMask))
                    {
                        m_target = target;
                    }
                }
            }
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
}
