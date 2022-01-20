using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
      public SpriteRenderer chSprite;

      

      public bool IsSit = false;
      public int currentJumpCount = 0;
      public bool isGrounded = false;
      public bool OnceJumpRayCheck = false;

      public bool Is_DownJump_GroundCheck = false;   // 다운 점프를 하는데 아래 블록인지 그라운드인지 알려주는 불값
      protected float m_MoveX;
      public Rigidbody2D m_rigidbody;
      protected BoxCollider2D m_BoxCollider;
      public BoxCollider2D m_GroundCollider;
       public Animator m_Anim;


      [Header("[Setting]")]
      public float MoveSpeed = 6;
      public int JumpCount = 2;
      public float jumpForce = 15f;
      public bool canDownJump;
      public bool cantJump;
      public float cantJumpTime;
      public bool cantMove;
      public float cantMoveTime;

      [Header("Audio")]
      public AudioSource audioSource;
      public AudioClip sfx_jump;
      public AudioClip sfx_warp;
      public AudioClip sfx_flare;

      protected void AnimUpdate()
      {


            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                  if (Input.GetKey(KeyCode.Mouse0))
                  {
                        m_Anim.Play("Attack");
                  }
                  else
                  {

                        if (m_MoveX == 0)
                        {
                              if (!OnceJumpRayCheck)
                                    m_Anim.Play("Idle");

                        }
                        else
                        {
                              m_Anim.Play("Run");
                        }

                  }
            }
      }

      public void Flip(bool bLeft)
      {
            chSprite.transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
      }


      protected void prefromJump()
      {
            Debug.Log("위 점프 Run,Falling false, Jump True");
            m_Anim.SetBool("Run", false);
            m_Anim.SetBool("Falling", false);
            m_Anim.SetBool("Jump",true);

            //audioSource.Stop();
            audioSource.PlayOneShot(sfx_jump);

            m_rigidbody.velocity = new Vector2(0, 0);

            m_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            OnceJumpRayCheck = true;
            isGrounded = false;

            currentJumpCount++;
      }
      protected IEnumerator CantMoveWhileWallJump()
      {
            cantMove = true;
            cantJump = true;
            yield return new WaitForSeconds(cantJumpTime);
            cantJump = false;
            yield return new WaitForSeconds(cantMoveTime - cantJumpTime);
            m_rigidbody.velocity = new Vector2(0, m_rigidbody.velocity.y);
            cantMove = false;
      }
      protected void LeftWallJump()
      {
            m_Anim.SetBool("Falling", false);
            m_Anim.SetBool("Jump", true);

            m_rigidbody.velocity = new Vector2(0, 0);

            Vector2 vec = new Vector2(0.5f, 1);
            m_rigidbody.AddForce(vec * jumpForce, ForceMode2D.Impulse);

            OnceJumpRayCheck = true;
            isGrounded = false;

            currentJumpCount++;
            StartCoroutine(CantMoveWhileWallJump());
      }
      protected void RightWallJump()
      {
            m_Anim.SetBool("Falling", false);
            m_Anim.SetBool("Jump", true);

            m_rigidbody.velocity = new Vector2(0, 0);

            Vector2 vec = new Vector2(-0.5f, 1);
            m_rigidbody.AddForce(vec * jumpForce, ForceMode2D.Impulse);

            OnceJumpRayCheck = true;
            isGrounded = false;

            currentJumpCount++;
            StartCoroutine(CantMoveWhileWallJump());
      }

      protected void DownJump()
      {
            if (canDownJump)
            {
                  if (!isGrounded)
                        return;

                  Debug.Log("아래 점프");
                  if (!Is_DownJump_GroundCheck)
                  {
                        //audioSource.Stop();
                        audioSource.PlayOneShot(sfx_jump);
                        //  m_Anim.Play("Jump");
                        Debug.Log("Run False, Jump True");
                        m_Anim.SetBool("Run", false);
                        m_Anim.SetBool("Jump", true);

                        m_rigidbody.AddForce(-Vector2.up * 10);
                        isGrounded = false;

                        //  OnceJumpRayCheck = true;

                        m_BoxCollider.enabled = false;
                        m_GroundCollider.enabled = false;

                        currentJumpCount++;

                        StartCoroutine(GroundCapsulleColliderTimmerFuc());

                  }
            }
      }

      IEnumerator GroundCapsulleColliderTimmerFuc()
      {
            yield return new WaitForSeconds(0.3f);
            m_BoxCollider.enabled = true;
            m_GroundCollider.enabled = true;
      }

      //////바닥 체크 레이케스트 
      Vector2 RayDir = Vector2.down;


      float PretmpY;
      float GroundCheckUpdateTic = 0;
      float GroundCheckUpdateTime = 0.01f;
      protected void GroundCheckUpdate()
      {
            
            if (!OnceJumpRayCheck)
                  return;

            GroundCheckUpdateTic += Time.deltaTime;

          

            if (GroundCheckUpdateTic > GroundCheckUpdateTime)
            {
                  GroundCheckUpdateTic = 0;


                  if (PretmpY == 0)
                  {
                        PretmpY = transform.position.y;
                        return;
                  }
                  float reY = transform.position.y - PretmpY;  //    -1  - 0 = -1 ,  -2 -   -1 = -3

                  if (reY < 0)
                  {
                        if (isGrounded)
                        {
                              //m_Anim.SetTrigger("Landing");
                              Debug.Log("reY < 0");
                              LandingEvent();
                              OnceJumpRayCheck = false;

                        }
                        else
                        {
                              m_Anim.SetBool("Run", false);
                              m_Anim.SetBool("Jump", false);
                              m_Anim.SetBool("Falling", true);
                       //       Debug.Log("Falling On, Jump,Run false");

                        }
                  }

                  if(reY == 0)
                  {
                        if (isGrounded)
                        {
                              //m_Anim.SetTrigger("Landing");
                              Debug.Log("reY = 0 " + reY);
                              OnceJumpRayCheck = false;
                        }
                  }

                  if (reY > 0)
                  {

                        if (isGrounded)
                        {
                              //m_Anim.SetTrigger("Landing");
                              Debug.Log("reY >= 0 " + reY);
                              LandingEvent();
                              OnceJumpRayCheck = false;
                              
                        }
                        else
                        {
                         //     m_Anim.SetTrigger("Falling");
                          //    Debug.Log("안부딪힘");

                        }
                  }
                  PretmpY = transform.position.y;

                  
            }
      }
      public abstract void LandingEvent();
}
