using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_lookAt;
    public Transform m_camTransform;
    public GameObject m_resetCamPos;
    private const float Y_MIN_ANGLE = -25.0f;
    private const float Y_MAX_ANGLE = 25.0f;

    private Camera m_cam;

    private float m_disFromPlayer = 10.0f;
    private float m_currentX = 0.0f;
    private float m_currentY = 0.0f;
    private Quaternion m_startingRotation;
    Vector3 m_camStartPos;
    Quaternion rotation;
    bool m_canBeReset = false;
    // Start is called before the first frame update
    void Start()
    {
        m_camTransform = transform;
        m_cam = Camera.main;
        m_camStartPos = new Vector3(0, 3, -10);
        m_startingRotation = m_camTransform.rotation;
    }

    private void Update()
    {
        m_currentX += Input.GetAxis("cameraRotHor");
        m_currentY += Input.GetAxis("cameraRotVer");

        m_currentY = Mathf.Clamp(m_currentY, Y_MIN_ANGLE, Y_MAX_ANGLE);
       

        
        if (m_camTransform.rotation != m_startingRotation)
        {
            m_canBeReset = true;
        }
        else
        {
            m_canBeReset = false;
        }
        CamMovement();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CamMovement();
    }
    private void CamMovement()
    {
        if (Input.GetButton("CamReset") && m_canBeReset)
        {
            Quaternion rotation1 = Quaternion.Euler(m_camTransform.transform.rotation.x, 0, 0);
            m_camTransform.position = rotation1 * m_resetCamPos.transform.position;
            m_camTransform.LookAt(m_lookAt.position);
            m_canBeReset = false;
            m_currentX = 0.0f;
            m_currentY = 0.0f;
            return;
        }
        
            rotation = Quaternion.Euler(m_currentY, m_currentX, 0);
            m_camTransform.position = m_lookAt.position + rotation * m_camStartPos;
            m_camTransform.LookAt(m_lookAt.position);
           
    }
}
