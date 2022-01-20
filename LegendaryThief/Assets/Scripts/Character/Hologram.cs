using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
      Player player;
      public float m_runSpeed = 4.5f;
      public float m_crouchSpeed;
      public float m_walkSpeed = 2.0f;
      public float m_jumpForce = 7.5f;
      public int maxJumpCount = 2;
      public int currentJumpCount = 0;
      public BoxCollider2D coll;   // offset 0 , -0.33   size 1.1 , 2.3
      // crouch - offset 0, -0.93  size 1.1, 1.1

      public float maxHealth;
      public float health;

      [Header("WallSlideJump")]
      public float wallSlideSpeed = -6f;
      public float wallHoldTime = 2f;

      public List<Deerg> deergs;

      public Animator m_animator;
      public Rigidbody2D m_body2d;
      private SpriteRenderer m_SR;
      private Sensor_Prototype m_groundSensor;
      private Sensor_Prototype m_wallSensorR1;
      private Sensor_Prototype m_wallSensorR2;
      private Sensor_Prototype m_wallSensorL1;
      private Sensor_Prototype m_wallSensorL2;
      private CrouchUpSensor crouchUpSensor;    // 기어다닐때 위에 뭐가있는지 확인.

      public bool m_grounded = false;
      public bool m_canControl = true;
      public bool m_moving = false;
      private bool m_movingY = false;
      private bool m_dead = false;
      private bool m_dodging = false;
      private bool m_ledgeClimb = false;

      public bool m_dash = false;
      public bool m_crouch = false;             //  웅크린 상태라면.
      public bool m_crouchMove = false;  // 웅크리고 움직이는 상태
      public bool m_crouchCanMove = false;     // 웅크리고 움직일 수 있는지?
      private bool m_wallSlide = false;
      private bool m_wallSlideTimeOut = false; // 벽 잡은후 일정 시간 지나면 미끄러지기.
      private bool m_wallCoroutineActive = false;      // 벽 타이머 이미 작동중인지.
      private bool m_wallClimb = false;
      public float m_wallClimbSpeed = 2f;
      private float m_dashRot = 0;
      private Vector3 m_climbPosition;
      private int m_facingDirection = 1;

      private float m_disableMovementTimer = 0.0f;
      private float m_disableJumpTimer = 0.0f;
      private float m_gravity;
      public float m_maxSpeed = 4.5f;
      public float m_dashCool = 0.5f;
      public bool isInvinsible;
      
      // Start is called before the first frame update


      void Start()
      {
            player = FindObjectOfType<Player>();
            m_animator = GetComponentInChildren<Animator>();
            m_body2d = GetComponent<Rigidbody2D>();
            m_SR = GetComponentInChildren<SpriteRenderer>();
            m_gravity = m_body2d.gravityScale;
          
            m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
            m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
            m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
            m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
            m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();
            crouchUpSensor = GetComponentInChildren<CrouchUpSensor>();
            health = maxHealth;

            crouchUpSensor.gameObject.SetActive(false);
      }


      // Update is called once per frame
      void Update()
      {
            if (m_body2d.velocity.y < -20f)
            {
                  //낙하 속도 제한
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, -20f);
            }


            // Decrease death respawn timer 
          
            //       m_dashCoolTime -= Time.deltaTime;

            // Decrease timer that disables input movement. Used when attacking
            m_disableMovementTimer -= Time.deltaTime;

            // Decrease timer that disables input jump. Used when wall Jump
            m_disableJumpTimer -= Time.deltaTime;

            
           

            if (m_dead)
                  return;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                  m_grounded = true;
                  m_animator.SetBool("Grounded", m_grounded);
                  currentJumpCount = 0;   // 점프 횟수 초기화
              
                  m_wallSlideTimeOut = false;
                  StopCoroutine("WallTimer");
                  m_wallCoroutineActive = false;
            }



            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                  m_grounded = false;
                  m_animator.SetBool("Grounded", m_grounded);
                  if (currentJumpCount == 0)
                  {
                        currentJumpCount++;
                  }
            }

            // -- Handle input and movement --
            float inputX = 0.0f;

            if (m_disableMovementTimer < 0.0f)
                  inputX = Input.GetAxis("Horizontal");

            // GetAxisRaw returns either -1, 0 or 1
            float inputRaw = Input.GetAxisRaw("Horizontal");

            // Check if character is currently moving

            if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            {
                  m_moving = true;
                  if (m_crouch)
                        m_crouchMove = true;
            }
            else
            {
                  m_moving = false;
                  if (m_crouch)
                        m_crouchMove = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                  m_moving = false;
                  if (m_crouch)
                        m_crouchMove = false;
                  inputX = 0f;
            }

            float inputY = 0.0f;

            if (m_disableMovementTimer < 0.0f)
                  inputY = Input.GetAxis("Vertical");

            float inputCol = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(inputY) != 0)
            {
                  m_movingY = true;
            }
            else if (Mathf.Abs(inputY) == 0)
            {
                  m_movingY = false;
            }


            float dashX = Input.GetAxisRaw("Horizontal");
            float dashY = Input.GetAxisRaw("Vertical");
            //  Debug.Log("input X " + inputX);
            /*    if (dashY > 0)
                {
                      m_dashYDirection = 1;
                }
                else if (dashY < 0)
                {
                      m_dashYDirection = -1;
                }
                else if (dashY == 0)
                {
                      m_dashYDirection = 0;
                }

                if (dashX > 0)
                {
                      m_dashXDirection = 1;
                }
                else if (dashX < 0)
                {
                      m_dashXDirection = -1;
                }
                else if (dashX == 0)
                {
                      m_dashXDirection = 0;
                }*/

            // Swap direction of sprite depending on move direction
            if (inputRaw > 0 && !m_dodging && !m_dash && !m_wallSlide && m_canControl && m_disableMovementTimer < 0.0f)
            {
                  //  if (inputRaw > 0 && !m_dodging && !m_dash && !m_wallSlide )
                  //        {
                  m_SR.flipX = false;
                  m_facingDirection = 1;
            }

            else if (inputRaw < 0 && !m_dodging && !m_dash && !m_wallSlide && m_canControl && m_disableMovementTimer < 0.0f)
            {
                  m_SR.flipX = true;
                  m_facingDirection = -1;
            }

            // SlowDownSpeed helps decelerate the characters when stopping
            float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
            // Set movement
            if (!m_dodging && !m_crouch && !m_ledgeClimb  && m_canControl && !m_wallSlide && !m_dash && m_disableMovementTimer < 0.0f)
            {
                  // 좌우 이동 velocity
                  m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);
                
            }
            if (m_canControl && m_wallSlide && m_disableMovementTimer < 0.0f)
            {
                  // 벽 기어오르기
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, inputY * m_wallClimbSpeed);
            }
            if (m_crouchCanMove && m_canControl)
            {
                  // 웅크린상태로 움직일때.
                  m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

            }

            // Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
           

            // 벽 잡기 

            // Check if all sensors are setup properly
            if (m_wallSensorR1 && m_wallSensorR2 && m_wallSensorL1 && m_wallSensorL2)
            {
                  bool prevWallSlide = m_wallSlide;


                  //Wall Slide
                  // True if either both right sensors are colliding and character is facing right
                  // OR if both left sensors are colliding and character is facing left

                  if (m_wallSlide == false)
                  {
                        // 처음 벽에 붙을떈 방향키 같이 눌러야함.
                        m_wallSlide = (Input.GetKey(KeyCode.RightArrow) && m_wallSensorR1.WallState() && m_wallSensorR2.WallState() && m_facingDirection == 1)
                         || (Input.GetKey(KeyCode.LeftArrow) && m_wallSensorL1.WallState() && m_wallSensorL2.WallState() && m_facingDirection == -1);
                  }
                  else
                  {
                        // 붙은 이후는 키입력 없이 유지.
                        m_wallSlide = (m_wallSensorR1.WallState() && m_wallSensorR2.WallState() && m_facingDirection == 1)
                               || (m_wallSensorL1.WallState() && m_wallSensorL2.WallState() && m_facingDirection == -1);
                  }
                  if (m_grounded)
                        m_wallSlide = false;
                  if (!prevWallSlide && m_wallSlide)
                  {
                        m_animator.SetTrigger("WallSlide");
                        m_animator.SetBool("WallHold", true);
                        Debug.Log("PrevWallSlide " + prevWallSlide);
                  }


                  //Play wall slide sound
                  if (prevWallSlide && !m_wallSlide)
                  {
                        Debug.Log("WallSlide End");

                        if (!m_ledgeClimb)
                        {

                              m_body2d.gravityScale = m_gravity;
                        }

                        PlayerAudioManager.instance.StopSound("WallSlide");
                        m_animator.SetBool("WallHold", false);
                        if (!m_grounded)
                              currentJumpCount++;
                  }
                  if (m_wallSlide && m_wallSlideTimeOut && !m_wallClimb)
                  {
                        // 시간 지나면 자동으로 내려감 조금씩.
                        // Debug.Log("WallSlideDown");
                        m_body2d.velocity = new Vector2(m_body2d.velocity.x, wallSlideSpeed);
                  }


                  //Grab Ledge
                  // True if either bottom right sensor is colliding and top right sensor is not colliding 
                  // OR if bottom left sensor is colliding and top left sensor is not colliding 

                  // 그랩 가능 여부
                  bool shouldGrab = !m_ledgeClimb && ((m_wallSensorR1.WallState() && !m_wallSensorR2.WallState()) || (m_wallSensorL1.WallState() && !m_wallSensorL2.WallState()));


                  if (shouldGrab && prevWallSlide)
                  {


                        Debug.Log("ShouldGrab : " + shouldGrab);
                        Vector3 rayStart;
                        if (m_facingDirection == 1)
                              rayStart = m_wallSensorR2.transform.position + new Vector3(0.3f, 0.0f, 0.0f);
                        else
                              rayStart = m_wallSensorL2.transform.position - new Vector3(0.3f, 0.0f, 0.0f);

                        var hit = Physics2D.Raycast(rayStart, Vector2.down, 1.0f);

                        GrabableLedge ledge = null;
                        if (hit)
                              ledge = hit.transform.GetComponent<GrabableLedge>();

                        if (ledge !=null)
                        {
                              m_ledgeClimb = true;
                              m_body2d.velocity = Vector2.zero;
                              m_body2d.gravityScale = 0;

                              m_climbPosition = ledge.transform.position + new Vector3(ledge.topClimbPosition.x, ledge.topClimbPosition.y, 0);
                              if (m_facingDirection == 1)
                                    m_climbPosition = ledge.transform.position + new Vector3(ledge.leftGrabPosition.x, ledge.leftGrabPosition.y, 0);
                              else
                                    m_climbPosition = ledge.transform.position + new Vector3(ledge.rightGrabPosition.x, ledge.rightGrabPosition.y, 0);
                              
                              m_animator.SetTrigger("LedgeClimb");
                              DisableWallSensors();


                              m_disableMovementTimer = 6.0f / 14.0f;
                        }

                        
                  }

                  bool prevCrouch = m_crouch;
                  if (m_grounded)
                  {
                        //  기어가기
                        if (m_crouch == false)
                        {
                              m_crouch = (Input.GetKey(KeyCode.RightArrow) && !m_wallSensorR1.WallState() && m_wallSensorR2.WallState() && m_facingDirection == 1)
                                    || (Input.GetKey(KeyCode.LeftArrow) && !m_wallSensorL1.WallState() && m_wallSensorL2.WallState() && m_facingDirection == -1);
                        }
                        else
                        {
                              //    Debug.Log("m_crouch = " + m_crouch);
                              // m_crouch = ( !m_wallSensorR1.State() && m_wallSensorR2.State() && m_facingDirection == 1)
                              //        || (!m_wallSensorL1.State() && m_wallSensorL2.State() && m_facingDirection == -1);
                              m_crouch = crouchUpSensor.GetState();

                        }
                  }
                  if (!prevCrouch && m_crouch)
                  {
                        Debug.Log("Crouch Start Update");
                        m_animator.SetTrigger("Crouch");
                        m_animator.SetBool("Crouching", true);
                        m_body2d.velocity = Vector2.zero;

                  }
                  if (prevCrouch && !m_crouch)
                  {
                        Debug.Log("Crouch End");

                        m_crouchCanMove = false;
                        m_crouchMove = false;
                        m_body2d.velocity = Vector2.zero;
                        m_canControl = false;
                        m_animator.SetBool("Crouching", false);

                  }
            }


            // -- Handle Animations --


            //Jump
            if (Input.GetButtonDown("Jump") && m_canControl && m_disableJumpTimer < 0.0f && !m_dodging && !m_dash && m_disableMovementTimer < 0.0f)
            {
                  if (currentJumpCount < maxJumpCount)
                  {
                        // Check if it's a normal jump or a wall jump
                        if (!m_wallSlide)
                        {
                              Debug.Log("Jump");
                              m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                            
                        }
                        else
                        {
                              // 벽 점프
                              //   m_wallSlideDown = false;
                              Debug.Log("WallSlideJump + " + m_facingDirection);
                              m_body2d.gravityScale = m_gravity;
                              //      if (inputX != m_facingDirection)
                              //       {
                              // 누르고 있는 키와 보고있는 방향이 다르면
                              // 반대 점프
                              m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce * 0.5f, m_jumpForce * 0.9f);
                              m_facingDirection = -m_facingDirection;
                              m_SR.flipX = !m_SR.flipX;
                              m_disableMovementTimer = 0.35f;
                              m_disableJumpTimer = 0.3f;
                              //         }
                              //        else if(inputX == m_facingDirection)
                              //        {
                              // 누르고 있는 키와 보고있는 방향이 같으면
                              // 같은방향 점프
                              //             m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce * 0.2f, m_jumpForce * 0.9f);
                              //           m_disableMovementTimer = 0.2f;
                              //           m_disableJumpTimer = 0.2f;
                              //    }

                              //m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce * 0.5f, m_jumpForce * 0.9f);

                              m_wallSensorR1.Disable(0.1f);
                              m_wallSensorL1.Disable(0.1f);

                              //m_facingDirection = -m_facingDirection;
                              //m_SR.flipX = !m_SR.flipX;

                        }

                        m_animator.SetTrigger("Jump");
                        m_grounded = false;
                        m_animator.SetBool("Grounded", m_grounded);
                        m_groundSensor.Disable(0.2f);
                        currentJumpCount++;
                  }
            }

           

            //Crouch / Stand up
            /* else if (Input.GetKeyDown("s") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && m_parryTimer < 0.0f)
             {
                   m_crouching = true;
                   m_animator.SetBool("Crouching", true);
                   m_body2d.velocity = new Vector2(m_body2d.velocity.x / 2.0f, m_body2d.velocity.y);
             }
             else if (Input.GetKeyUp("s") && m_crouching)
             {
                   m_crouching = false;
                   m_animator.SetBool("Crouching", false);
             }*/

           
            else if (m_movingY && m_canControl && m_wallSlide)
            {
                  // 벽 기어오르기
                  m_animator.SetBool("WallClimb", true);
            }
            else if (!m_movingY && m_wallSlide)
            {
                  m_animator.SetBool("WallClimb", false);
            }

            else if (m_crouchMove && m_canControl)
            {
                  m_animator.SetBool("CrouchWalk", m_crouchMove);
                  m_maxSpeed = m_crouchSpeed;
            }

            //Run
            else if (m_moving && m_canControl)
            {
                  m_animator.SetInteger("AnimState", 1);
                  m_maxSpeed = m_runSpeed;
            }

            //Idle
            else
            {
                  m_animator.SetInteger("AnimState", 0);
                  m_animator.SetBool("WallClimb", false);
                  m_animator.SetBool("CrouchWalk", false);
            }

      }



      public void CrouchStart()
      {
            // crouch - offset 0, -0.93  size 1.1, 1.1
            coll.offset = new Vector2(0, -0.93f);
            coll.size = new Vector2(1.1f, 1.1f);
            m_crouchCanMove = true;
            crouchUpSensor.isTouch = true;
            crouchUpSensor.gameObject.SetActive(true);
      }

      public void CrouchEnd()
      {
            // offset 0 , -0.33   size 1.1 , 2.3
            coll.offset = new Vector2(0, -0.33f);
            coll.size = new Vector2(1.1f, 2.3f);
            m_body2d.velocity = Vector2.zero;
            m_canControl = true;
            crouchUpSensor.gameObject.SetActive(false);
            crouchUpSensor.isTouch = true;
      }


      public void ResetDash()
      {
            Debug.Log("ResetDash");
            //   m_animator.SetTrigger("DashEnd");
            m_animator.SetBool("DashBool", false);
            m_dash = false;
            if (m_body2d.velocity.y > 0)
            {
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, 4.5f);
            }


            m_body2d.gravityScale = m_gravity;
            m_dashRot = 0f;
      }
    
      // Function used to spawn a dust effect
      // All dust effects spawns on the floor
      // dustXoffset controls how far from the player the effects spawns.
      // Default dustXoffset is zero
      public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
      {
            if (dust != null)
            {
                  // Set dust spawn position
                  Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
                  GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
                  // Turn dust in correct X direction
                  newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
            }
      }
      public void SpawnDustEffectRotation(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
      {
            if (dust != null)
            {
                  // Set dust spawn position
                  Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
                  GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
                  newDust.transform.rotation = Quaternion.Euler(0, 0, m_dashRot);
                  // Turn dust in correct X direction
                  newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
            }
      }

      void DisableWallSensors()
      {
            Debug.Log("DisableWallSensors");

            currentJumpCount = maxJumpCount - 1;

            m_wallSlide = false;
         
            m_wallSensorR1.Disable(0.8f);
            m_wallSensorR2.Disable(0.8f);
            m_wallSensorL1.Disable(0.8f);
            m_wallSensorL2.Disable(0.8f);
            
            m_animator.SetBool("WallHold", false);

      }

      public void SetWallslideGravity()
      {

            //m_body2d.gravityScale = wallSlideGravity;
            if (m_wallSlide)
            {
                  Debug.Log("SetWallSlideGravity");
                  m_body2d.gravityScale = 0f;
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, 0f);
                  currentJumpCount = maxJumpCount - 1;
                  //     m_wallSlideDown = true;
                  StartCoroutine("WallTimer");
            }

      }

      private IEnumerator WallTimer()
      {
            if (!m_wallCoroutineActive)
            {
                  // 코루틴이 이미 작동중인지 확인.
                  Debug.Log("WallTimer Active");
                  m_wallCoroutineActive = true;

                  yield return new WaitForSeconds(wallHoldTime);

                  //    m_wallCoroutineActive = false;
                  m_wallSlideTimeOut = true;
            }

            yield return null;
      }

      // Called in AE_resetDodge in PrototypeHeroAnimEvents
      public void ResetDodging()
      {
            m_dodging = false;
      }

      public void SetPositionToClimbPosition()
      {
            transform.position = m_climbPosition;
            m_body2d.gravityScale = m_gravity;
            m_wallSensorR1.Disable(3.0f / 14.0f);
            m_wallSensorR2.Disable(3.0f / 14.0f);
            m_wallSensorL1.Disable(3.0f / 14.0f);
            m_wallSensorL2.Disable(3.0f / 14.0f);
           m_ledgeClimb = false;
      }

      public bool IsWallSliding()
      {
            return m_wallSlide;
      }

      public void DisableMovement(float time = 0.0f)
      {
            m_disableMovementTimer = time;
      }

      public void HologramEnd()
      {
            // 시간이 지나서 홀로그램 끝남.
            Debug.Log("Hologram End");
            Destroy(gameObject);
      }
      public void HologramHitEnd()
      {
            // 맞아서 홀로그램 취소됨.
            Debug.Log("HologramHitEnd");
            player.MeditationEnd();
            Destroy(gameObject);
      }
      public virtual void OnDamage(float damage, Vector2 hitPoint)
      {
            if (isInvinsible == false)    // 무적이 아니면
            {
                  // 데미지만큼 체력 감소
                  health -= damage;
                  // HpBarRefresh(health);
                  //체력이 0 이하 && 아직 죽지않았다면 사망처리

                  StartCoroutine(Invinsible(1f));
                  if (health <= 0)
                  {
                        HologramHitEnd();
                  }

            }
            // hit = true; // 맞을때 멈추게 하거나 하는 용도
            //    StartCoroutine(HitEffect());
      }
      
      public void ActiveControl()
      {
            m_canControl = true;
      }
      public void DisableControl()
      {
            m_canControl = false;
      }
      public IEnumerator Invinsible(float sec)
      {
            isInvinsible = true;
            yield return new WaitForSeconds(sec);

            isInvinsible = false;
      }
  
      public bool IsGround()
      {
            return m_grounded;
      }

}
