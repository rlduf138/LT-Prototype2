using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterBase : PlayerController
{

      private Stage01 stage;
      public bool isRightWall;
      public bool isLeftWall;

      public bool isRightHold;
      public bool isLeftHold;

      public bool isHide;
      public GameObject flareEffect;
      

      [Header("Warp")]
      public GameObject markerPrefab;
      public GameObject marker;

      [Header("UI")]
      public Slider hideSlider;

      [Header("Status")]
      public float health;
      public bool dead;
      public Action onDeath;
      public float hideTime;
      public float hideCurrentTime;
      public bool isInvinsible;     // 무적 여부.

      [Header("HoldWall")]
      public bool canHold;
      public bool isHold;
      public float holdingTime;
      public float slipSpeed;
      public float originGravityScale;

      [Header("Tutorial")]
      public bool isTuto;
      public bool canJump;    // 튜토리얼 씬 1
      public bool canWarp;    // 튜토리얼 씬 2
      public bool warpTuto;
      public bool canMove;


      private void Start()
      {

            m_BoxCollider = this.transform.GetComponent<BoxCollider2D>();
            //   m_Anim = this.transform.Find("ch").GetComponent<Animator>();
            m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
            stage = FindObjectOfType<Stage01>();
            //StartCoroutine(RigidInterpolate());
      }
      public IEnumerator RigidInterpolate()
      {
            yield return new WaitForSeconds(3f);
            m_rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
      }

      public void OffSprite()
      {
            chSprite.enabled = false;

      }
      public void OnSprite()
      {
            chSprite.enabled = true;
      }

      private void Update()
      {
            if (!isTuto && !dead)
            {
                  checkInput();
            }
            /*if (m_rigidbody.velocity.magnitude > 30)
            {
                  m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);

            }*/
           if(m_rigidbody.velocity.y < -30)
           {
                  m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, -30f);
                  Debug.Log("velocity : " + m_rigidbody.velocity + "magnitude : " + m_rigidbody.velocity.magnitude);
                  // 낙하 속도 제한
            }
            
      }
      private void FixedUpdate()
      {
            if (!isTuto && !dead)
            {
               //   CheckFixedInput();
            }
      }

      public IEnumerator HoldWall()
      {
            currentJumpCount = 0;
            isHold = true;
            StartCoroutine("HoldTimer");
            m_rigidbody.gravityScale = 0f;
            while (isHold)
            {
                  yield return new WaitForFixedUpdate();
                  if (Input.GetKey(KeyCode.DownArrow))
                  {
                        m_rigidbody.velocity = new Vector2(0, -2f);
                  }
                  else { m_rigidbody.velocity = new Vector2(0, 0); }

            }
            EndHold();
      }
      public IEnumerator HoldTimer()
      {
            yield return new WaitForSeconds(holdingTime);
            isHold = false;
      }
      public void EndHold()
      {
            StopCoroutine("HoldWall");
            StopCoroutine("HoldTimer");
            isHold = false;
            m_rigidbody.gravityScale = originGravityScale;
      }

      public virtual void OnDamage(float damage, Vector2 hitPoint)
      {
            if (isInvinsible == false)    // 무적이 아니면
            {
                  // 데미지만큼 체력 감소
                  health -= damage;
                  // HpBarRefresh(health);
                  //체력이 0 이하 && 아직 죽지않았다면 사망처리
                  if (health <= 0 && !dead)
                  {
                        Die();
                  }
                  else
                  {
                        StartCoroutine(Invinsible(3f));
                        
                        stage.CharacterDameged();
                  }
            }
            // hit = true; // 맞을때 멈추게 하거나 하는 용도
            //    StartCoroutine(HitEffect());
      }
      public IEnumerator Invinsible(float sec)
      {
            isInvinsible = true;
            yield return new WaitForSeconds(sec);

            isInvinsible = false;
      }
      public virtual void Die()
      {
            // 사망시 이벤트.
            dead = true;
            onDeath?.Invoke();
      }

      public void CheckFixedInput()
      {
            if (canMove && Input.GetKey(KeyCode.RightArrow))
            {

                  if (isGrounded)  // 땅바닥에 있었을때. 
                  {
                        //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        //      return;

                        m_Anim.SetBool("Run", true);
                        m_Anim.SetBool("Jump", false);
                        if (!isRightWall)
                        {
                              transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

                        }

                  }
                  else
                  {
                        if (!isRightWall)
                              transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

                  }

                  //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                  //      return;

                  if (!Input.GetKey(KeyCode.LeftArrow))
                        Flip(true);

            }
            else if (Input.GetKey(KeyCode.LeftArrow) && canMove)
            {

                  if (isGrounded)  // 땅바닥에 있었을때. 
                  {
                        // if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        //       return;
                        m_Anim.SetBool("Run", true);
                        m_Anim.SetBool("Jump", false);
                        if (!isLeftWall)
                        {
                              transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

                        }
                  }
                  else
                  {
                        if (!isLeftWall)
                              transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                  }

                  // if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                  //       return;

                  if (!Input.GetKey(KeyCode.RightArrow))
                        Flip(false);

            }
       /*     if (Input.GetKeyDown(KeyCode.C))
            {
                  //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                  //      return;
                  if (canJump == true)    // 튜토리얼
                  {
                        if (currentJumpCount < JumpCount)  // 0 , 1
                        {

                              if (!IsSit)
                              {
                                    prefromJump();
                              }
                              else
                              {
                                    DownJump();
                              }
                        }
                  }
            }*/
      }
      public void checkInput()
      {

            /*  if (Input.anyKeyDown)
              {
                    if (isHide)
                    {
                          if (!Input.GetKeyDown(KeyCode.X))
                          {
                                HideOff();
                          }
                    }
              }*/
            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                  if (isHide)
                        HideOff();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))  //아래 버튼 눌렀을때. 
            {

                  IsSit = true;
                  //    m_Anim.Play("Sit");


            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {

               //   m_Anim.Play("Idle");
                  IsSit = false;

            }


            // sit나 die일때 애니메이션이 돌때는 다른 애니메이션이 되지 않게 한다. 
            /*   if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
               {
                     if (Input.GetKeyDown(KeyCode.Space))
                     {
                           if (currentJumpCount < JumpCount)  // 0 , 1
                           {
                                 DownJump();
                           }
                     }

                     return;
               }
            */

            m_MoveX = Input.GetAxis("Horizontal");



            GroundCheckUpdate();


            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                  if (Input.GetKey(KeyCode.Mouse0))
                  {


                        // m_Anim.Play("Attack");
                  }
                  else
                  {

                        if (m_MoveX == 0)
                        {
                              if (!OnceJumpRayCheck)
                              {

                                    //  m_Anim.Play("Idle");
                                    // m_Anim.SetBool("Run", false);
                              }

                        }
                        else
                        {
                              // m_Anim.SetBool("Run", true);
                              //m_Anim.Play("Run");
                        }

                  }
            }


            if (Input.GetKey(KeyCode.Alpha1))
            {
                  // m_Anim.Play("Die");

            }
            
            // 기타 이동 인풋.

            
            if (canMove && Input.GetKey(KeyCode.RightArrow) )
            {
                  if (!cantMove)
                  {
                        if (isGrounded)  // 땅바닥에 있었을때. 
                        {
                              //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                              //      return;

                              m_Anim.SetBool("Run", true);
                              m_Anim.SetBool("Jump", false);
                              if (!isRightWall)
                              {
                                    transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

                              }

                        }
                        else
                        {
                              if (!isRightWall)
                              {
                                    transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                                    if (isHold)
                                    {
                                          EndHold();
                                    }

                              }
                              if (isRightHold)
                              {
                                    if (!isHold)
                                          StartCoroutine("HoldWall");
                              }

                        }

                        //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        //      return;

                        if (!Input.GetKey(KeyCode.LeftArrow))
                              Flip(true);
                  }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && canMove)
            {
                  if (!cantMove)
                  {
                        if (isGrounded)  // 땅바닥에 있었을때. 
                        {
                              // if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                              //       return;
                              m_Anim.SetBool("Run", true);
                              m_Anim.SetBool("Jump", false);
                              if (!isLeftWall)
                              {
                                    transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

                              }
                        }
                        else
                        {
                              if (!isLeftWall)
                              {
                                    transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                                    if (isHold)
                                    {
                                          EndHold();
                                    }
                              }
                              if (isLeftHold)
                              {
                                    if (!isHold)
                                          StartCoroutine("HoldWall");
                              }
                        }

                        // if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        //       return;

                        if (!Input.GetKey(KeyCode.RightArrow))
                              Flip(false);
                  }
            }

            
            //========================

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                  // 이동키 떼면 애니메이션 멈춤
                  m_Anim.SetBool("Run", false);
                //  audioSource.Stop();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                  //if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                  //      return;
                  if (canJump == true)    // 튜토리얼
                  {
                        if (!cantJump)
                        {
                              if (currentJumpCount < JumpCount)  // 0 , 1
                              {
                                    if (isHold && isLeftWall)
                                    {
                                          EndHold();
                                          LeftWallJump();
                                    }
                                    else if (isHold && isRightWall)
                                    {
                                          EndHold();
                                          RightWallJump();
                                    }
                                    else if (!IsSit)
                                    {
                                          prefromJump();
                                    }
                                    else
                                    {
                                          DownJump();
                                    }

                              }
                        }
                  }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                  // 숨기 버튼 클릭
                  if (canWarp == true)    // 튜토리얼
                  {
                        //if (isGrounded && !isHide)
                       // {
                              Debug.Log("X Key DOwn");
                              //Hide();
                              Warp();
                        //}
                  }
            }
      }

      public void Warp()
      {
            // 마커 박아서 워프.
            if(marker == null)
            {
                  Debug.Log("Marker null");
                  // 마커가 없으면 마커를 생성
                  marker = Instantiate(markerPrefab, transform.localPosition, Quaternion.identity);
                  marker.GetComponent<WarpMarker>().Setting(this);
            }
            else
            {
                  Debug.Log("Warp To Marker");
                  transform.position = marker.transform.position;
                  Destroy(marker, 1f);
            }
      }

      public void Hide()
      {
            m_Anim.SetBool("Run", false);
            StartCoroutine("HideOn");
       //     yield return new WaitForSeconds(hideTime);
       //     HideOff();
      }
      public IEnumerator HideOn()
      {
            Debug.Log("HideOn");
            canMove = false;
            m_Anim.SetBool("Hide", true);
            isHide = true;
            //  chSprite.color = new Color(1, 1, 1, 0.3f);
            gameObject.tag = "Untagged";

            // 슬라이더 세팅
            hideCurrentTime = hideTime;
          //  hideSlider.gameObject.SetActive(true);
            hideSlider.maxValue = hideTime;
            hideSlider.value = hideTime;

            while (hideCurrentTime > 0)
            {
                  yield return new WaitForFixedUpdate();
                  hideCurrentTime -= Time.deltaTime;
                  hideSlider.value = hideCurrentTime;
            }
            hideSlider.gameObject.SetActive(false);
            HideOff();
      }
      public void HideOff()
      {
            StopCoroutine("HideOn");
            hideSlider.gameObject.SetActive(false);
            canMove = true;
            m_Anim.SetBool("Hide", false);
            isHide = false;
            // chSprite.color = new Color(1, 1, 1, 1);
            gameObject.tag = "Player";
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if(collision.tag == "Money")
            {
                  // Money 발견.
                  Debug.Log("Money Find");
                  Destroy(collision.gameObject);
            }
      }
      public void FlareShot()
      {
            m_Anim.Play("Flare");
            Vector2 pos = new Vector2(transform.position.x+0.15f, transform.position.y + 6.8f);
            Instantiate(flareEffect, pos, Quaternion.identity);
            audioSource.PlayOneShot(sfx_flare);
      }
      public void WarpSound()
      {
            audioSource.PlayOneShot(sfx_warp);
      }
      public override void LandingEvent()
      {

            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                  Debug.Log("LandingEvent");
                  m_Anim.SetBool("Falling", false);
                  m_Anim.SetBool("Jump", false);
                  m_Anim.SetTrigger("Landing");
                  //m_Anim.Play("Idle");
            }
      }

}
