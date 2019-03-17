using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fow.DirectionFromAngle(-fow.m_viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirectionFromAngle(fow.m_viewAngle / 2, false);
        Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.m_viewAngle, fow.m_viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.m_viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.m_viewRadius);

        Handles.color = Color.red;
        //foreach(Transform visibleTarget in fow.visibleTargets)
        //{
        //    Handles.DrawLine(fow.transform.position, visibleTarget.position);
        //}

        if(fow.m_target != null)
        {
            Handles.DrawLine(fow.transform.position, fow.m_target.transform.position);
        }

    }
}
