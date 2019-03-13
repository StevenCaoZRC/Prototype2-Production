using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharaMove : MonoBehaviour
{
    public float m_speed = 10.0f;
    public float m_slowSpeed = 25.0f;
    public float m_backSpeed = 25.0f;
    public float m_maxSpeed = 15.0f;
    public float m_rotSpeed = 100.0f;
    public GameObject shield;
    float m_fowardVelocity;
    float m_acceleration;
    float m_deceleration;
    float m_braking;
    Quaternion m_targetRotation;
    Rigidbody m_rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        m_rotSpeed = 75.0f;
        m_rigidBody = GetComponent<Rigidbody>();
        m_fowardVelocity = 0;
        m_acceleration =  m_speed/2.5f;
        m_deceleration = -m_slowSpeed / 1.5f;
        m_braking = -m_backSpeed / 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Movecharacter();
        
    }

    void Movecharacter()
    {

        // Debug.Log(m_speed);

        //var ver = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;

        //var braking = Input.GetAxis("Vertical") * -m_speed/m_speed * Time.deltaTime;
        //// Vector3 position = new Vector3(0.0f, 0.0f, ver) * m_speed * Time.deltaTime;
        //Vector3 direction = new Vector3(hor, 0.0f, ver);

        //if (Input.GetAxis("Vertical") > 0.51f)
        //{
        //    m_speed = 15.0f;
        //    Debug.Log("speeding up");
        //    transform.Translate(0, 0, ver);
        //}
        //else if (Input.GetAxis("Vertical") < 0.49f)
        //{
        //    m_speed = 4.0f;
        //    Debug.Log("slowing down");
        //    transform.Translate(0, 0, ver);
        //}
        //else if (Input.GetAxis("Vertical") == 0.0f)
        //{
        //    transform.Translate(0, 0, braking);
        //}
       
        //if (GetComponent<PlayerControl>().m_disabledInput == false)
        //{
           
            float hor = Input.GetAxis("Horizontal") * m_rotSpeed * Time.deltaTime;
            if (Input.GetAxis("Vertical") > 0.1)
            {
                m_fowardVelocity += m_acceleration * Time.deltaTime;
                m_fowardVelocity = Mathf.Min(m_fowardVelocity, m_maxSpeed);
                m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            }
            else if (Input.GetAxis("Vertical") < -0.1)
            {
                // Debug.Log("Backwards");
                m_fowardVelocity += m_deceleration * Time.deltaTime;
                m_fowardVelocity = Mathf.Max(m_fowardVelocity, -m_backSpeed);
                m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            }
            else if (m_rigidBody.velocity != Vector3.zero)
            {
                //Debug.Log("Slowing Down");
                m_fowardVelocity += m_braking * Time.deltaTime;
                m_fowardVelocity = Mathf.Max(m_fowardVelocity, 0);
                m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            }
            //Debug.Log(m_rigidBody.velocity);
            transform.Rotate(0, hor, 0);
        //}
    }
}
