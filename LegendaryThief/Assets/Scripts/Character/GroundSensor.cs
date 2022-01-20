using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
      public PlayerController m_root;

      // Use this for initialization
      void Start()
      {
            m_root = this.transform.root.GetComponent<PlayerController>();
      }

      ContactPoint2D[] contacts = new ContactPoint2D[1];

      private void OnTriggerEnter2D(Collider2D other)
      {
            if (other.CompareTag("Ground") || other.CompareTag("Platform") || other.CompareTag("CantDownPlatform")) 
            {
                  StartCoroutine(CheckLandingActive());

            }

      }

      protected IEnumerator CheckLandingActive()
      {
            // 착지 모션이 안나오는것에 대비
            float checkTime = 0.1f;
            float currentTime = 0;

            while(checkTime >= currentTime)
            {
                  if(m_root.m_Anim.GetCurrentAnimatorStateInfo(0)
                        .IsName("Falling"))
                  {
                        currentTime += Time.deltaTime;
                  }
                  else
                  {
                        currentTime = 33f;
                  }
                  yield return new WaitForFixedUpdate();
            }
            if(currentTime != 33f)
            {
                  m_root.LandingEvent();
            }

            yield return null;
      }

      void OnTriggerStay2D(Collider2D other)
      {
            if (other.CompareTag("Wall") || other.CompareTag("Ground") ||other.CompareTag("Platform") || other.CompareTag("CantDownPlatform"))
            {
                  if (other.CompareTag("Wall") || other.CompareTag("Ground") || other.CompareTag("CantDownPlatform"))
                  {
                        m_root.Is_DownJump_GroundCheck = true;
                  }
                  else
                  {
                        m_root.Is_DownJump_GroundCheck = false;
                  }

                  if (m_root.m_rigidbody.velocity.y <= 0)
                  {
                        m_root.isGrounded = true;
                        m_root.currentJumpCount = 0;

                  }
            }
      }

      void OnTriggerExit2D(Collider2D other)
      {
            if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Platform") || other.CompareTag("CantDownPlatform"))
            {
                  m_root.isGrounded = false;

                  // 점프없이 낙하 가정.
                  Debug.Log("GroundSensor. TriggerExit   Run False");
                  // m_root.m_Anim.SetBool("Run", false);
                  m_root.OnceJumpRayCheck = true;

                  //if (!m_root.OnceJumpRayCheck && !m_root.m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                  //       m_root.m_Anim.SetTrigger("Falling");

                  // m_root.OnceJumpRayCheck = true;
                  //  m_root.currentJumpCount++;
            }
      }
}
