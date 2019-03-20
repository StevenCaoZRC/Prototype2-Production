using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour
{
    #region Booleans
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

    public bool m_isBlocking = false;
    public bool m_isDead = false;
    private bool m_isHit = false;
    public bool m_LevelCleared = false;
    bool finished = false;
    public bool m_disabledInput = false;

    #endregion
    #region GameObjects
    //Particles
    public GameObject m_chargeReachedParticles;
   
    public GameObject m_spear;  //reference to the spear object
    public GameObject m_shield; //reference to the player shield object
    

    public GameObject m_chargingParticles;

    public Animator m_playerAnimator;
    public Animator m_horseAnimator;
    #endregion
    Rigidbody rb;
    #region Health and Stamina varables
    //Helath and Stamina
    public Health m_playerHealth;
    public Stamina m_playerStamina;
    public float m_normalAttackCost = 10.0f;
    public float m_SpecialAttackCost = 40.0f;
    #endregion
    #region Controller Vibration
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    #endregion
    private TempCharaMove m_playerMove;
    private float m_normalSpeed = 5.0f;
    private float m_boostSpeed = 10.0f;
    private float m_rotSpeed = 2.0f;
    Vector3 m_velocity = Vector3.zero;
    float m_chargeHoldTimer = 0.0f;
    float m_chargeHoldRequired = 2.0f;
    public float _y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);
        rb = GetComponent<Rigidbody>();

        m_isDead = false;
        m_isHit = false;
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
        FindPlayerController();
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
    void FindPlayerController()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
    public void PlayerAttack()
    {
        if(!m_spear.GetComponent<SpearAttack>().GetIsAttacking())
        {
            if (GameManager.GetAxisOnce(ref m_isAttacking, "Spear") 
                && !m_spear.GetComponent<SpearAttack>().GetIsAttacking() 
                && !m_isHit && !m_isCharging
                && m_playerStamina.m_currentStamina >= m_normalAttackCost)
            {
                m_playerAnimator.SetTrigger("NormalAttack"); //Start attack anim
                m_spear.GetComponent<SpearAttack>().SetAttacking(true); //Start attack mechanic

                //m_spear.GetComponent<SpearAttack>().NormalAttack(); //Start attack mechanic
                m_playerStamina.m_currentStamina -= 10.0f;

            }
            else if(Input.GetAxis("ChargeSpear") > 0f
                && !m_isHit
                && !m_spear.GetComponent<SpearAttack>().GetIsAttacking()
                && m_playerStamina.m_currentStamina >= m_SpecialAttackCost) //If player is charging
            {
                m_isCharging = true;
                m_chargeHoldTimer += Time.deltaTime; //Increase charge timer
                m_chargingParticles.SetActive(true); //Activate charging particles
                if(m_playerAnimator.GetBool("ChargeSpear") == false)
                {
                    m_playerAnimator.SetBool("ChargeSpear", true);
                }

                //If X is held more than 3 seconds, charge attack ready
                if (m_chargeHoldTimer >= m_chargeHoldRequired)
                {
                    m_isholdingCharge = true;
                    m_chargeReachedParticles.SetActive(true);
                }
            }
            else
            {
                m_chargeHoldTimer = 0.0f;
                m_isCharging = false;
                m_chargingParticles.SetActive(false);
                m_chargeReachedParticles.SetActive(false);
                m_playerAnimator.SetBool("ChargeSpear", false);
            }

            //If charge is ready, use special attack
            if (Input.GetAxis("ChargeSpear") == 0f && m_isholdingCharge)
            {
                m_playerAnimator.GetComponent<Animator>().SetTrigger("SpecialAttack");
                m_playerStamina.m_currentStamina -= 60.0f;

                ChargeAttack();
            }
        }
    }

    public void PlayerBlock()
    {
       // Debug.Log("blockref: " + m_blocked + " is blocking: " + m_shield.GetComponent<PlayerShield>().GetIsBlocking());

        if (GameManager.GetAxisOnce(ref m_blocked, "Shield"))
        {
            if (m_playerStamina.m_currentStamina >= 20.0f && !m_shield.GetComponent<PlayerShield>().GetIsBlocking())
            {
                m_playerAnimator.SetTrigger("Block"); //Start attack anim
                m_playerStamina.m_currentStamina -= 10.0f;

                //m_shield.GetComponent<PlayerShield>().Block();
                //Debug.Log(m_shield.GetComponent<ShieldBlock>().GetIsColliding()); 

            }
        }
      
    }

    public void PlayerMove()
    {
        if (!m_isDead)
        {

            float _x = Input.GetAxisRaw("Horizontal");
            float _z = Input.GetAxisRaw("Vertical");
             _y = Input.GetAxisRaw("Horizontal");

            Vector3 _moveX = transform.right * _x;
            Vector3 _moveZ = transform.forward * _z;
            Vector3 _rotation = new Vector3(0, _y, 0) * m_rotSpeed;

            if (_x != 0.0f || _z != 0.0f || _y != 0.0f)
            {
                if (!m_playerAnimator.GetBool("Running"))
                {
                    m_playerAnimator.SetBool("Walking", true);
                    m_horseAnimator.SetBool("Walking", true);
                }
            }
            else
            {
                m_playerAnimator.SetBool("Walking", false);
                m_playerAnimator.SetBool("Running", false);
                m_horseAnimator.SetBool("Walking", false);
                m_horseAnimator.SetBool("Running", false);

            }

            //Calculating Player movement
            if (Input.GetAxisRaw("Boost") > 0)
            {
                m_velocity = (_moveX + _moveZ).normalized * m_boostSpeed;
                if (m_velocity != Vector3.zero)
                {
                    m_playerAnimator.SetBool("Running", true);
                    m_playerAnimator.SetBool("Walking", false);
                    m_horseAnimator.SetBool("Running", true);
                    m_horseAnimator.SetBool("Walking", false);
                }
            }
            else
            {
                m_velocity = (_moveX + _moveZ).normalized * m_normalSpeed;
                m_playerAnimator.SetBool("Running", false);
                m_horseAnimator.SetBool("Running", false);
            }

            if (_z <= -0.1f) //if going backwards
            {
                m_velocity *= 0.5f;
            }

            //Apply Movement
            m_playerMove.SetMovement(m_velocity);

            //Calculating Player Rotation

            //Apply Rotation of Player
            m_playerMove.SetRotation(_rotation);
        }
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

    public void TakeDamage(GameObject _attackedFrom)
    {
        if(m_shield.GetComponent<PlayerShield>().GetIsBlocking())
        {
            m_shield.GetComponent<PlayerShield>().Block();
            m_shield.GetComponent<PlayerShield>().PlayParticles(true);

            //if (m_shield.GetComponent<PlayerShield>().CheckCol())
            //{
            Debug.Log("PUSHED");
                StartCoroutine(PushBackPlayer());
            //}

        }
        else if (m_playerHealth.m_currentHealth > 0 && !m_isHit)
        {
            m_isHit = true;

            m_spear.GetComponent<SpearAttack>().EndAttack(); //Start attack mechanic

            m_playerAnimator.SetTrigger("IsHit");
            m_playerHealth.m_currentHealth -= _attackedFrom.GetComponent<EnemyWeapon>().GetAttackDamage();//_attackedFrom.GetComponent<SpearAttack>().GetDamage();
            Debug.Log("Player health: " + m_playerHealth.m_currentHealth);

            var moveDirection = rb.transform.position - _attackedFrom.transform.position;
            rb.AddForce(moveDirection.normalized * 500f);
            StartCoroutine(ResetHit());

            if (m_playerHealth.m_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    IEnumerator ResetHit()
    {
        GamePad.SetVibration(playerIndex,0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
        m_isHit = false;
    }

    IEnumerator PushBackPlayer()
    {
        finished = true;
        m_shield.GetComponent<PlayerShield>().m_knockedBack = true;
        rb.AddForce(-transform.forward.normalized * 500.0f, ForceMode.Impulse);
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
        //m_spear.GetComponent<SpearAttack>().ChargeAttack();
        //Debug.Log("suPERSAIYAN");
        m_spear.GetComponent<SpearAttack>().SetAttacking(true);
        //Reset timer values
        m_chargeHoldTimer = 0;
        m_isCharging = false;
        m_isholdingCharge = false;
        m_chargeReachedParticles.SetActive(false);
        m_chargingParticles.SetActive(false);
    }

    public bool GetIsDead()
    {
        return m_isDead;
    }

    public void Die()
    {
        //Play dead anim
        //Spawn particles
        StartCoroutine(DeathAnimation());
    }

    protected IEnumerator DeathAnimation()
    {
        m_isDead = true;
        if (m_playerAnimator != null)
        {
            m_playerAnimator.SetTrigger("Dead");
        }
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    public GameObject GetWeapon()
    {
        return m_spear;
    }

    public GameObject GetShield()
    {
        return m_shield;
    }
}
