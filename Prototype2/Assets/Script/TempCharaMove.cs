using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharaMove : MonoBehaviour
{
    //Please remain hidden
    [HideInInspector]
    public float m_speed = 10.0f;
    [HideInInspector]
    public float m_slowSpeed = 25.0f;
    [HideInInspector]
    public float m_backSpeed = 25.0f;
    [HideInInspector]
    public float m_maxSpeed = 15.0f;
    [HideInInspector]
    public float m_rotSpeed = 100.0f;
    [HideInInspector]
    public bool m_canMove = true;


    float m_fowardVelocity; //the forward velocity, this is the multipler I use to add on to the velocity
    float m_acceleration;   //acceleration for when the player is moving forward
    float m_deceleration;   //Used when player is moving backwards
    float m_braking;        //This is used for when the player is giving zero input and is gradually slowing down 

    Quaternion m_targetRotation;
    public Rigidbody m_rigidBody; 
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
    void Update()
    {
        
        Movecharacter();
        
    }

    void Movecharacter()
    {
        if (m_canMove)
        {
            //Rotating the player
            float hor = Input.GetAxis("Horizontal") * m_rotSpeed * Time.deltaTime;
            float ver = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
            transform.Translate(0, 0, ver);
            //Movement 
            //Moving forwards and backwards
            //if (Input.GetAxis("Vertical") > 0.1)
            //{

            //    m_fowardVelocity += m_acceleration * Time.deltaTime;
            //    m_fowardVelocity = Mathf.Min(m_fowardVelocity, m_maxSpeed);
            //    m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            //}
            //else if (Input.GetAxis("Vertical") < -0.1)
            //{

            //    m_fowardVelocity += m_deceleration * Time.deltaTime;
            //    m_fowardVelocity = Mathf.Max(m_fowardVelocity, -m_backSpeed);
            //    m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            //    // m_rigidBody.AddForce(transform.forward.normalized * m_fowardVelocity);

            //}
            //else if (m_rigidBody.velocity != Vector3.zero)
            //{
            //    //Debug.Log("Slowing Down");
            //    m_fowardVelocity += m_rigidBody.velocity.x * Time.deltaTime;
            //    m_fowardVelocity = Mathf.Max(m_fowardVelocity, 0);
            //    m_rigidBody.velocity = transform.forward * m_fowardVelocity;
            //}
            //else
            //{
            //    m_rigidBody.velocity = Vector3.zero;
            //}
            transform.Rotate(0, hor, 0);
        }
    }
}
