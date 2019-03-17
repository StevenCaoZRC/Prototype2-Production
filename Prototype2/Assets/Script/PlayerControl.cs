using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool m_isAttacking = false;
    [HideInInspector]
    public bool m_isCharging = false;
    [HideInInspector]
    public bool m_isholdingCharge = false;
    [HideInInspector]
    public bool m_blocked = false;
    [HideInInspector]
    public bool m_pushedBack = false;

    bool finished = false;
   
    public GameObject m_spear;  //reference to the spear object
    public GameObject m_shield; //reference to the player shield object
    public GameObject m_chargeReachedParticles; 
    public GameObject m_chargingParticles;
    public Animator m_playerAnimator;
    public bool m_disabledInput = false;
    public Rigidbody rb;
    public Health m_playerHealth;
    public Stamina m_playerStamina;

    private TempCharaMove m_playerMove;
    private float m_normalSpeed = 5.0f;
    private float m_boostSpeed = 10.0f;
    private float m_rotSpeed = 2.0f;
    Vector3 _velocity = Vector3.zero;
    float m_chargeHoldTimer = 0.0f;
    float m_chargeHoldRequired = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);
        rb = GetComponent<Rigidbody>();
        m_playerMove = GetComponent<TempCharaMove>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PlayerAttack();
        PlayerBlock();
    }

    private void Update()
    {
        PlayerMove();

        KnockbackListener();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_playerHealth.m_currentHealth -= 10.0f;
        }
        else if (Input.GetKeyDown(KeyCode.X) && m_playerHealth.m_currentHealth < m_playerHealth.m_maxHealth)
        {
            m_playerHealth.m_currentHealth += 10.0f;
        }
        if (m_playerHealth.m_currentHealth > m_playerHealth.m_maxHealth)
        {
            m_playerHealth.m_currentHealth = m_playerHealth.m_maxHealth;
        }
    }

    public void PlayerAttack()
    {
        if(!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (GameManager.GetAxisOnce(ref m_isAttacking, "Spear") && !m_isCharging)
            {
                //lerp position forward and back
                m_spear.GetComponent<SpearAttack>().NormalAttack(m_playerAnimator);

            }
            else if(Input.GetAxis("ChargeSpear") > 0f)
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime;
                m_chargingParticles.SetActive(true);
                Debug.Log("uh oh");
                if(m_playerAnimator.GetBool("ChargeSpear") == false)
                {
                    m_playerAnimator.SetBool("ChargeSpear", true);
                }
                //If X is held more than 3 seconds, charge attack ready
                if (m_chargeHoldTimer >= m_chargeHoldRequired)
                {
                    m_isholdingCharge = true;
                    m_chargeReachedParticles.SetActive(true);
                    //m_playerAnimator.SetBool("IsCharging", true);

                }
            }
            else
            {
                m_chargeHoldTimer = 0;
                m_isCharging = false;
                m_chargingParticles.SetActive(false);
                m_playerAnimator.SetBool("ChargeSpear", false);
                //Debug.Log("Let go");
            }

            //If charge is ready, use special attack
            if (Input.GetAxis("ChargeSpear") == 0f && m_isholdingCharge)
            {
                ChargeAttack();
            }
        }
    }

    public void PlayerBlock()
    {
        
        if (GameManager.GetAxisOnce(ref m_blocked, "Shield"))
        {
            if (m_playerStamina.m_currentStamina >= 20.0f)
            { m_playerStamina.m_currentStamina -= 20.0f;     }
            if (!m_shield.GetComponent<PlayerShield>().GetIsBlocking() && m_playerStamina.m_currentStamina >= 20.0f)
            {
                m_shield.GetComponent<PlayerShield>().PlayerBlock();
                //Debug.Log(m_shield.GetComponent<ShieldBlock>().GetIsColliding()); 
                if (m_shield.GetComponent<PlayerShield>().CheckCol())
                {
                    Debug.Log("PUSHED");
                    StartCoroutine(PushBackPlayer());

                }
                
            }
        }
      
    }

    public void PlayerMove()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _z = Input.GetAxisRaw("Vertical");

        Vector3 _moveX = transform.right * _x;
        Vector3 _moveZ = transform.forward * _z;
      
        //Calculating Player movement
        if (Input.GetAxisRaw("Boost") > 0)
        {
            _velocity = (_moveX + _moveZ).normalized * m_boostSpeed ;
        }
        else
        {
            _velocity = (_moveX + _moveZ).normalized * m_normalSpeed  ;
        }
        //Apply Movement
        m_playerMove.GetMovement(_velocity);

        //Calculating Player Rotation
        float _y = Input.GetAxisRaw("Horizontal");
        Vector3 _rotation = new Vector3(0, _y, 0) * m_rotSpeed;

        //Apply Rotation of Player
        m_playerMove.GetRotation(_rotation);

       
    }

  
    void KnockbackListener()
    {
        if (m_shield.GetComponent<PlayerShield>().m_knockedBack)
        {
            GetComponent<TempCharaMove>().m_canMove = false;
        }
        else if (m_shield.GetComponent<PlayerShield>().m_knockedBack && !finished)
        {
            //Input.ResetInputAxes();
            Debug.Log("Success");
            GetComponent<TempCharaMove>().m_canMove = true;
            m_shield.GetComponent<PlayerShield>().m_knockedBack = false;
        }
    }

    IEnumerator PushBackPlayer()
    {
        finished = true;
        m_shield.GetComponent<PlayerShield>().m_knockedBack = true;
        rb.AddForce(-transform.forward.normalized * 20.0f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Debug.Log("Success");
        m_shield.GetComponent<PlayerShield>().m_knockedBack = false;
      
        GetComponent<TempCharaMove>().m_canMove = true;
        Input.ResetInputAxes();
       
       

        yield return null;
    }

    void ChargeAttack()
    {
        m_spear.GetComponent<SpearAttack>().ChargeAttack(m_playerAnimator);
        Debug.Log("suPERSAIYAN");

        //Reset timer values
        m_chargeHoldTimer = 0;
        m_isCharging = false;
        m_isholdingCharge = false;
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);

    }
}
