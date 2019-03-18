using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_lookAt;
    public Transform m_camTransform;
    public Transform m_resetCamPos;
    private const float Y_MIN_ANGLE = -50.0f;
    private const float Y_MAX_ANGLE = 25.0f;

    private const float X_MIN_ANGLE = -30.0f;
    private const float X_MAX_ANGLE = 30.0f;


    private Camera m_cam;

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
        m_camStartPos = new Vector3(0, 4, -6);
        //m_resetCamPos.transform.position = new Vector3(0, 2f, -4);
        //rotation = Quaternion.Euler(15, 0, 0);
        m_camTransform.rotation = Quaternion.Euler(15, 0, 0);
        m_startingRotation = m_camTransform.rotation;

    }

    private void Update()
    {
        m_currentX += Input.GetAxis("cameraRotHor");
        m_currentY += Input.GetAxis("cameraRotVer");

        m_currentY = Mathf.Clamp(m_currentY, Y_MIN_ANGLE, Y_MAX_ANGLE);
        // m_currentX = Mathf.Clamp(m_currentX, X_MIN_ANGLE, X_MAX_ANGLE);
        
    }

        // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButton("CamReset"))
        {
            Quaternion rotation1 = Quaternion.Euler(m_resetCamPos.rotation.x, m_resetCamPos.rotation.y, m_resetCamPos.rotation.z);
            m_camTransform.position = rotation1 * m_resetCamPos.position;
            m_camTransform.LookAt(m_lookAt.position);

            m_currentX = 0.0f;
            m_currentY = 0.0f;
            return;
        }
        CamMovement();

    }
    private void CamMovement()
    {

        
        rotation = Quaternion.Euler(m_currentY, m_currentX, 0);
            m_camTransform.position = m_lookAt.position + rotation * m_camStartPos;
            m_camTransform.LookAt(m_lookAt.position );



    }
}
