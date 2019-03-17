using UnityEngine;

public class TempCharaMove : MonoBehaviour
{
    //Please remain hidden
    //[HideInInspector]
  
    

    [HideInInspector]
    public bool m_canMove = true;
    private Vector3 m_velocity = Vector3.zero;
    private Vector3 m_rotation = Vector3.zero;
    private Vector3 m_camRotation = Vector3.zero;
    [SerializeField]
    public Camera cam;


    Quaternion m_targetRotation;
    public Rigidbody m_rigidBody;
    // Start is called before the first frame update
    void Start()
    {
      
        m_rigidBody = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Movecharacter();

    }
    //Gets Movement vector of the character
    public void GetMovement(Vector3 _velocity)
    {

        m_velocity = _velocity;
    }

    //Gets Rotation vector of the character
    public void GetRotation(Vector3 _rotation)
    {

        m_rotation = _rotation;
    }

    //Gets Rotation vector of the character
    public void Movecharacter()
    {
        if (m_canMove)
        {
            if (m_velocity != Vector3.zero)
            {
                m_rigidBody.MovePosition(m_rigidBody.position + m_velocity * Time.fixedDeltaTime);
            }

            m_rigidBody.MoveRotation(m_rigidBody.rotation * Quaternion.Euler(m_rotation));

            //Rotating the player
            //float hor = Input.GetAxis("Horizontal") * m_rotSpeed * Time.deltaTime;
            //float ver = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
        
            //transform.Translate(0,0,ver);
            ////Trial 1:
            //transform.Rotate(0, hor, 0);
            //if (Input.GetAxis("Vertical") > 0)
            //{
            //    m_rigidBody.AddForce(transform.forward * m_speed * Time.deltaTime, ForceMode.VelocityChange);
            //}
            //else if (Input.GetAxis("Vertical") < 0)
            //{
            //    m_rigidBody.AddForce(-transform.forward * m_speed * Time.deltaTime, ForceMode.VelocityChange);
            //}
            //else
            //{
            //    m_rigidBody.AddForce(m_braking * Time.deltaTime, 0, m_braking * Time.deltaTime, ForceMode.VelocityChange); 
            //}
            
        }
        
    }
}
