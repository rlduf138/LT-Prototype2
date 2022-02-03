using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
      public float m_runSpeed = 4.5f;
      public float m_walkSpeed = 2.0f;
      public float m_jumpForce = 7.5f;
      public int maxJumpCount = 2;
      public int currentJumpCount = 0;


      public float maxHealth;
      public float health;
      [Header("StageInfo")]
      public StageInfo prevStageinfo;     // 다음 맵으로 넘어갈때 변경.

      [Header("Jewel")]
      public int jewelCount;

      [Header("Dash")]
      public float dashPower;
      public int maxDashCount;
      public int currentDashCount;
      public GameObject dashEffect;

      [Header("WallSlideJump")]
      public float wallSlideSpeed = -6f;

      public List<Deerg> deergs;

      public Animator m_animator;
      public Rigidbody2D m_body2d;
      private SpriteRenderer m_SR;
      private Sensor_Prototype m_groundSensor;
      private Sensor_Prototype m_wallSensorR1;
      //  private Sensor_Prototype m_wallSensorR2;
      private Sensor_Prototype m_wallSensorL1;
      //  private Sensor_Prototype m_wallSensorL2;
      public bool m_grounded = false;
      public bool m_canControl = true;
      private bool m_moving = false;
      private bool m_dead = false;
      private bool m_dodging = false;
      public bool m_wallSlide = false;
      private bool m_wallTouch = false;
      public bool m_wallSlideDown = false;
      public bool m_dash = false;
      public bool m_wallHit = false;
      
      private float m_dashRot = 0;
      private Vector3 m_climbPosition;
      private int m_facingDirection = 1;
      private int m_dashYDirection = 0;
      private int m_dashXDirection = 0;

      private float m_disableMovementTimer = 0.0f;
      private float m_respawnTimer = 0.0f;
      private float m_disableJumpTimer = 0.0f;
      private float m_jumpToWallTime = 0.0f;
      private float m_wallHoldTime = 0.0f;
      private float m_dashCoolTime = 0.0f;
      private Vector3 m_respawnPosition = Vector3.zero;
      private float m_gravity;
      public float m_maxSpeed = 4.5f;
      public float m_dashCool = 0.5f;
      public bool isInvinsible;
      Stage01 stage;
      // Start is called before the first frame update


      void Start()
      {

            m_animator = GetComponentInChildren<Animator>();
            m_body2d = GetComponent<Rigidbody2D>();
            m_SR = GetComponentInChildren<SpriteRenderer>();
            m_gravity = m_body2d.gravityScale;
            stage = FindObjectOfType<Stage01>();

            m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
            m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
            //       m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
            m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
            //   m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();

            health = maxHealth;
      }


      // Update is called once per frame
      void Update()
      {
            if (m_body2d.velocity.y < -30f)
            {
                  //낙하 속도 제한
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, -30f);
            }


            // Decrease death respawn timer 
            m_respawnTimer -= Time.deltaTime;

            m_dashCoolTime -= Time.deltaTime;

            // Decrease timer that disables input movement. Used when attacking
            m_disableMovementTimer -= Time.deltaTime;

            // Decrease timer that disables input jump. Used when wall Jump
            m_disableJumpTimer -= Time.deltaTime;

            m_jumpToWallTime -= Time.deltaTime;

            // Respawn Hero if dead
            if (m_dead && m_respawnTimer < 0.0f)
                  RespawnHero();

            if (m_dead)
                  return;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                  m_grounded = true;
                  m_animator.SetBool("Grounded", m_grounded);
                  currentJumpCount = 0;   // 점프 횟수 초기화
                  currentDashCount = 0;
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
                  m_moving = true;
            else
                  m_moving = false;
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                  m_moving = false;
                  inputX = 0f;
            }

            float dashX = Input.GetAxisRaw("Horizontal");
            float dashY = Input.GetAxisRaw("Vertical");
            //  Debug.Log("input X " + inputX);
            if (dashY > 0)
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
            }

            // Swap direction of sprite depending on move direction
            if (inputRaw > 0 && !m_dodging && !m_dash && !m_wallSlide && m_disableMovementTimer < 0.0f)
            {
          //  if (inputRaw > 0 && !m_dodging && !m_dash && !m_wallSlide )
          //        {
                        m_SR.flipX = false;
                  m_facingDirection = 1;
            }

            else if (inputRaw < 0 && !m_dodging && !m_dash && !m_wallSlide && m_disableMovementTimer < 0.0f)
            {
                  m_SR.flipX = true;
                  m_facingDirection = -1;
            }

            // SlowDownSpeed helps decelerate the characters when stopping
            float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
            // Set movement
            if ( !m_dodging && m_canControl && !m_wallSlide && !m_dash && m_disableMovementTimer < 0.0f)
            {
                  // 좌우 이동 velocity
                  m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

            }
            // Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
            if(currentDashCount != 0 && m_grounded)
            {
                  currentDashCount = 0;
            }

            // Check if all sensors are setup properly
            if (m_wallSensorR1 && m_wallSensorL1)
            {
                  bool prevWallSlide = m_wallSlide;
                  //Wall Slide
                  // True if either both right sensors are colliding and character is facing right
                  // OR if both left sensors are colliding and character is facing left
               
                  m_wallTouch = (m_wallSensorR1.State() && m_facingDirection == 1) || (m_wallSensorL1.State() && m_facingDirection == -1);
                  m_wallSlide = (m_wallSensorR1.WallState() && m_facingDirection == 1) || (m_wallSensorL1.WallState() && m_facingDirection == -1);
                
                  if (m_grounded)
                        m_wallSlide = false;
                  if (m_jumpToWallTime < 0.0f)
                  {
                        m_animator.SetBool("WallSlide", m_wallSlide);   // 바로 벽에 못 붙게하기위해.
                  }
                  if ( (m_wallTouch ||m_wallSlide) && m_dash)
                  {
                        Debug.Log("대쉬 중 벽 터치");
                       
                        m_animator.SetBool("DashBool", false);
                      //  m_animator.SetBool("WallSlide", m_wallSlide);
                        m_dash = false;
                        //m_body2d.velocity = new Vector2(m_body2d.velocity.x, 4.5f);
                        m_body2d.velocity = Vector2.zero;
                        m_body2d.gravityScale = m_gravity;
                        m_dashRot = 0f;

                  }
                  if (prevWallSlide && !m_wallSlide)
                  {
                        Debug.Log("WallSlide End");
                        if (m_wallSlideDown)
                              m_body2d.gravityScale = m_gravity;
                        m_wallSlideDown = false;
                        PlayerAudioManager.instance.StopSound("WallSlide");
                        if (!m_grounded)
                              currentJumpCount++;
                  }
                  if (m_wallSlide)
                  {
                      //  currentDashCount = 0;
                        if (m_facingDirection == 1)
                        {
                              // 오른쪽 보고있으면.
                              if (inputX < 0)
                              {
                                    // 타이머 작동하여 벽에서 떨어트림.
                                    m_wallHoldTime += Time.deltaTime;
                                    if (m_wallHoldTime > 0.4f)
                                    {
                                          Debug.Log("벽에서 떨구기 " + m_wallHoldTime);
                                          m_wallSlide = false;
                                          m_body2d.velocity = new Vector2(-10f, 0f);
                                          if (m_wallSlideDown)
                                                m_body2d.gravityScale = m_gravity;
                                          m_wallSlideDown = false;
                                          currentJumpCount++;
                                    }
                              }
                              else
                              {
                                    m_wallHoldTime = 0f;
                              }
                        }
                        else if (m_facingDirection == -1)
                        {
                              // 왼쪽 보고있으면.
                              if (inputX > 0)
                              {
                                    // 타이머 작동하여 벽에서 떨어트림.
                                    m_wallHoldTime += Time.deltaTime;
                                    if (m_wallHoldTime >0.4f)
                                    {
                                          Debug.Log("벽에서 떨구기 " + m_wallHoldTime);
                                          m_wallSlide = false;
                                          m_body2d.velocity = new Vector2(10f, 0f);
                                          if (m_wallSlideDown)
                                                m_body2d.gravityScale = m_gravity;
                                          m_wallSlideDown = false;
                                          currentJumpCount++;
                                    }
                              }
                              else
                              {
                                    m_wallHoldTime = 0f;
                              }
                        }
                  }

                  //Play wall slide sound


                  if (m_wallSlideDown)
                  {
                        // Debug.Log("WallSlideDown");
                        m_body2d.velocity = new Vector2(m_body2d.velocity.x, wallSlideSpeed);

                  }
            }

            // -- Handle Animations --

            // Dash
            if (Input.GetKeyDown("x") && maxDashCount > currentDashCount && m_dashCoolTime <=0 && !m_wallSlide && m_canControl)
            {
                  StartCoroutine(WallHitTime());
                  Debug.Log("Dash Input");
                  m_dash = true;
                  //m_animator.SetTrigger("Dash");
                  // 대쉬 쿨타임
                  m_dashCoolTime = m_dashCool;
                  
                  m_animator.SetBool("DashBool", true);
                  m_animator.SetTrigger("Dash");
                  m_body2d.gravityScale = 0f;
                  if (m_dashXDirection == 0 && m_dashYDirection == 0)
                  {
                        m_body2d.velocity = new Vector2(m_facingDirection * dashPower, 0);
                  }
                  else
                  {
                        if(m_dashXDirection > 0)
                        {
                              m_SR.flipX = false;
                        }else if (m_dashXDirection < 0)
                        {
                              m_SR.flipX = true;
                        }
                        m_body2d.velocity = new Vector2(m_dashXDirection * dashPower, m_dashYDirection * dashPower * 1.2f);
                  }
                  if (m_dashYDirection == 1 && m_dashXDirection == 0)
                  {
                        // 위 대쉬
                        m_dashRot = 90f;
                  }
                  if (m_dashYDirection == 1 && m_dashXDirection == 1)
                  {
                        // 우상
                        m_dashRot = 45f;
                  }
                  if (m_dashYDirection == -1 && m_dashXDirection == 1)
                  {
                        // 우하
                        m_dashRot = -45f;
                  }
                  if (m_dashYDirection == -1 && m_dashXDirection == 0)
                  {
                        // 하
                        m_dashRot = -90f;
                  }
                  if (m_dashYDirection == 1 && m_dashXDirection == -1)
                  {
                        // 좌상
                        m_dashRot = 135f;
                  }
                  if (m_dashYDirection == 0 && m_dashXDirection == -1)
                  {
                        // 좌
                        m_dashRot = 180f;
                  }
                  if (m_dashYDirection == -1 && m_dashXDirection == -1)
                  {
                        // 좌하
                        m_dashRot = -135f;
                  }
                  currentDashCount++;
                //  m_groundSensor.Disable(0.4f);
            }

            //Jump
            else if (Input.GetButtonDown("Jump") && m_canControl && m_disableJumpTimer < 0.0f && !m_dodging && !m_dash && m_disableMovementTimer < 0.0f)
            {
                  if (currentJumpCount < maxJumpCount)
                  {
                        // Check if it's a normal jump or a wall jump
                        if (!m_wallSlide)
                        {
                              Debug.Log("Jump");
                              m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                              m_jumpToWallTime = 0.08f;
                        }
                        else
                        {
                              // 벽 점프
                              m_wallSlideDown = false;
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

            //Run
            else if (m_moving && m_canControl)
            {
                  m_animator.SetInteger("AnimState", 1);
                  m_maxSpeed = m_runSpeed;
            }

            //Idle
            else
                  m_animator.SetInteger("AnimState", 0);

      }

      public IEnumerator WallHitTime()
      {
            m_wallHit = true;
            yield return new WaitForSeconds(0.7f);
            m_wallHit = false;
      }

      public IEnumerator StageMove(Transform endTransform)
      {
            m_canControl = false;
            var t = 0f;
            var start = transform.position;
            var end = endTransform.position;
            AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);


            while (t < 0.5f)
            {
                  // m_animator.SetInteger("AnimState", 1);
                  t += Time.deltaTime / 1f;
                  transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  yield return null;
            }
            m_canControl = true;
      }

      public void ResetDash()
      {
            Debug.Log("ResetDash");
            //   m_animator.SetTrigger("DashEnd");
            m_animator.SetBool("DashBool", false);
            m_dash = false;
            if(m_body2d.velocity.y > 0)
            {
                  m_body2d.velocity = new Vector2(m_body2d.velocity.x, 4.5f);
            }

            
            m_body2d.gravityScale = m_gravity;
            m_dashRot = 0f;
      }
      public void DashEffect()
      {
            if (dashEffect != null)
            {
                  Vector3 effectSpawnPosition = transform.position + new Vector3(0f, 0.3f);
                  GameObject effect = Instantiate(dashEffect, effectSpawnPosition, Quaternion.identity);
                  effect.transform.localScale = effect.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
                  // Destroy(effect, 0.3f);
            }
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
            m_wallSlide = false;
            m_wallSensorR1.Disable(0.8f);
            m_wallSensorL1.Disable(0.8f);

            m_body2d.gravityScale = 0f;
            currentJumpCount = maxJumpCount - 1;
            m_wallSlideDown = true;
            //m_body2d.gravityScale = m_gravity;
            m_animator.SetBool("WallSlide", m_wallSlide);

      }

      public void SetWallslideGravity()
      {
            //        Debug.Log("SetWallSlideGravity");
            //m_body2d.gravityScale = wallSlideGravity;
            if (m_wallSlide)
            {
                  m_body2d.gravityScale = 0f;
                  currentJumpCount = maxJumpCount - 1;
                  m_wallSlideDown = true;
            }

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
            m_wallSensorL1.Disable(3.0f / 14.0f);

      }

      public bool IsWallSliding()
      {
            return m_wallSlide;
      }

      public void DisableMovement(float time = 0.0f)
      {
            m_disableMovementTimer = time;
      }

      void RespawnHero()
      {
            transform.position = Vector3.zero;
            m_dead = false;
            m_animator.Rebind();
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

                  stage.CharacterDameged();

            }
            // hit = true; // 맞을때 멈추게 하거나 하는 용도
            //    StartCoroutine(HitEffect());
      }
      public void ResetCharacter()
      {
            for (int i = 0; i < deergs.Count; i++)
            {
                  deergs.RemoveAt(0);
            }
            m_body2d.gravityScale = m_gravity;
            currentJumpCount = 0;
            m_dash = false;
            m_animator.Play("Idle");
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
      public void JewelAdd()
      {
            jewelCount++;
      }
      public bool IsGround()
      {
            return m_grounded;
      }

      //=======================Test=========================
      public void TestSetting(float _jumpForce, float _gravityScale, float _moveSpeed, float _wallSlideSpeed, float _dashPower)
      {
            m_jumpForce = _jumpForce;
            m_gravity = _gravityScale;
            m_body2d.gravityScale = _gravityScale;
            m_runSpeed = _moveSpeed;
            wallSlideSpeed = _wallSlideSpeed;
            dashPower = _dashPower;
      }
      public float TestGetJumpForce()
      {
            return m_jumpForce;
      }
      public float TestGetGravity()
      {
            return m_gravity;
      }
      public float TestGetMoveSpeed()
      {
            return m_runSpeed;
      }
      public float TestGetSlideSpeed()
      {
            return wallSlideSpeed;
      }
      public float TestGetDashPower()
      {
            return dashPower;
      }

      public void TestSetJumpForce(float jumpForce)
      {
            m_jumpForce = jumpForce;
      }
      public void TestSetGravity(float gravity)
      {
            m_gravity = gravity;
      }
      public void TestSetMoveSpeed(float movespeed)
      {
            m_runSpeed = movespeed;
      }
      public void TestSetSlideSpeed(float slideSpeed)
      {
            wallSlideSpeed = slideSpeed;
      }
      public void TestSetDashPower(float _dashPower)
      {
            this.dashPower = _dashPower;
      }
}
