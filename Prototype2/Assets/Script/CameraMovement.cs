using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform m_cameraPivot;
    public GameObject m_player;

    private const float Y_MIN_ANGLE = -35.0f;
    private const float Y_MAX_ANGLE = 60.0f;

    public float m_sensitivity = 1.0f;

    private float m_currentX = 0.0f;
    private float m_currentY = 0.0f;
 
    // Start is called before the first frame update
    void Start()
    {

    }

        // Update is called once per frame
    void LateUpdate()
    {
        CamMovement();
    }
    private void CamMovement()
    {
        m_currentX += Input.GetAxis("cameraRotHor");// * m_sensitivity * Time.deltaTime;
        m_currentY += Input.GetAxis("cameraRotVer");// * m_sensitivity* Time.deltaTime;
        m_cameraPivot.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 2.5f, m_player.transform.position.z);



        m_currentY = Mathf.Clamp(m_currentY, Y_MIN_ANGLE, Y_MAX_ANGLE);

        transform.LookAt(m_cameraPivot);
    
        if (m_currentX != 0)
        {
            m_cameraPivot.rotation = Quaternion.Euler(m_currentY, m_currentX, 0);
        }

        if (Input.GetButtonDown("CamReset"))
        {
            m_currentX = 0;
            m_currentY = 0;

            m_cameraPivot.rotation = Quaternion.Euler(m_player.transform.rotation.x, m_player.transform.rotation.y, 0);
           // m_cameraPivot.position = m_player.transform.GetChild(0).transform.position;
            m_cameraPivot.forward = m_player.transform.forward;
            
        }
    }
}
