using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_lookAt;
    public Transform m_camTransform;

    private const float Y_MIN_ANGLE = -25.0f;
    private const float Y_MAX_ANGLE = 25.0f;

    private Camera m_cam;

    private float m_disFromPlayer = 10.0f;
    private float m_currentX = 0.0f;
    private float m_currentY = 0.0f;
    Vector3 m_camStartPos;
   
    bool Resetpos = false;
    // Start is called before the first frame update
    void Start()
    {
        m_camTransform = transform;
        m_cam = Camera.main;
        m_camStartPos = new Vector3(0,3,-10);
        
    }

    private void Update()
    {
        m_currentX += Input.GetAxis("cameraRotHor");
        m_currentY += Input.GetAxis("cameraRotVer");

        m_currentY = Mathf.Clamp(m_currentY, Y_MIN_ANGLE, Y_MAX_ANGLE);
        
    }
    // Update is called once per frame
    void LateUpdate()
    {

       
        Quaternion rotation = Quaternion.Euler(m_currentY, m_currentX, 0);
        m_camTransform.position = m_lookAt.position + rotation * m_camStartPos;
        m_camTransform.LookAt(m_lookAt.position);
        
       
       
       
       
    }
    private void ResetCamPos()
    {
        Quaternion rotation = Quaternion.Euler(0,0,0);
        m_camTransform.rotation = rotation;
        m_camTransform.position = m_lookAt.position + rotation * new Vector3(0, 3, -10);

    }
}
