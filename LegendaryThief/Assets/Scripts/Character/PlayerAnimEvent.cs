using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
      // References to effect prefabs. These are set in the inspector
      public bool isHologram = false;

      [Header("Effects")]
      public GameObject m_RunStopDust;
      public GameObject m_JumpDust;
      public GameObject m_LandingDust;
      public GameObject m_DodgeDust;
      public GameObject m_WallSlideDust;
      public GameObject m_WallJumpDust;
      public GameObject m_DashDust;
     
      private Player m_player;
      private Hologram m_hologram;
      private PlayerAudioManager m_audioManager;

      // Start is called before the first frame update
      void Start()
    {
            if (isHologram)
                  m_hologram = GetComponentInParent<Hologram>();
            else
                  m_player = GetComponentInParent<Player>();
            m_audioManager = PlayerAudioManager.instance;
      }

      // Animation Events
      // These functions are called inside the animation files
      void AE_resetDodge()
      {
            m_player.ResetDodging();
            float dustXOffset = 0.6f;
            float dustYOffset = 0.078125f;
            if(isHologram)
                  m_hologram.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
            else
                  m_player.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
      }

      void AE_setPositionToClimbPosition()
      {
            if(isHologram)
                  m_hologram.SetPositionToClimbPosition();
            else
            m_player.SetPositionToClimbPosition();
      }

      void AE_runStop()
      {
            m_audioManager.PlaySound("RunStop");
            float dustXOffset = 0.6f;
            float dustYOffset = 0.078125f;
            if(isHologram)
                  m_hologram.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
            else
                  m_player.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
      }

      void AE_footstep()
      {
            m_audioManager.PlaySound("Footstep");
      }

      void AE_Jump()
      {
            m_audioManager.PlaySound("Jump");
            if (isHologram)
            {
                  if (!m_hologram.IsWallSliding())
                  {
                        float dustYOffset = 0.078125f;
                        m_hologram.SpawnDustEffect(m_JumpDust, 0.0f, dustYOffset);
                  }
                  else
                  {
                        m_hologram.SpawnDustEffect(m_WallJumpDust);
                  }
            }
            else
            {
                  if (!m_player.IsWallSliding())
                  {
                        float dustYOffset = 0.078125f;
                        m_player.SpawnDustEffect(m_JumpDust, 0.0f, dustYOffset);
                  }
                  else
                  {
                        m_player.SpawnDustEffect(m_WallJumpDust);
                  }
            }
      }

      void AE_Landing()
      {
            m_audioManager.PlaySound("Landing");
            float dustYOffset = 0.078125f;
            if(isHologram)
                  m_hologram.SpawnDustEffect(m_LandingDust, 0.0f, dustYOffset);
            else
                  m_player.SpawnDustEffect(m_LandingDust, 0.0f, dustYOffset);
      }

      void AE_DashEnd()
      {
            //m_player.SpawnDustEffectRotation(m_DashDust, 0.0f, dustYOffset);
            m_player.DashEffect();
            m_player.ResetDash();
      }
      void AE_Dash()
      {
           // Debug.Log("AE_Dash");
            m_audioManager.PlaySound("Dash");
            float dustYOffset = 0f;
            m_player.SpawnDustEffectRotation(m_DashDust, 0.0f, dustYOffset);
            m_player.DashEffect();
      }
      void AE_DashIng()
      {
            m_player.DashEffect();
      }
    
      void AE_Hurt()
      {
            m_audioManager.PlaySound("Hurt");
      }

      void AE_Death()
      {
            m_audioManager.PlaySound("Death");
      }

      


      void AE_Dodge()
      {
            m_audioManager.PlaySound("Dodge");
            float dustYOffset = 0.078125f;
            m_player.SpawnDustEffect(m_DodgeDust, 0.0f, dustYOffset);
      }

      void AE_WallSlide()
      {
            Debug.Log("WallSlide");
            //m_audioManager.GetComponent<AudioSource>().loop = true;
            if (!m_audioManager.IsPlaying("WallSlide"))
                  m_audioManager.PlaySound("WallSlide");
            
            if(isHologram)
                  m_hologram.SetWallslideGravity();
            else
                  m_player.SetWallslideGravity();
            //m_player.SpawnDustEffect(m_WallSlideDust, dustXOffset, dustYOffset);
      }

      void AE_CrouchStart()
      {
            if (isHologram)
            {
                  m_hologram.CrouchStart();
            }else
                  m_player.CrouchStart();
      }
      void AE_CrouchEnd()
      {
            if (isHologram)
            {
                  m_hologram.CrouchEnd();
            }else
                  m_player.CrouchEnd();
      }
      void AE_LedgeGrab()
      {
            m_audioManager.PlaySound("LedgeGrab");
      }

      void AE_LedgeClimb()
      {
            m_audioManager.PlaySound("RunStop");
      }

      void AE_MeditationEnd()
      {
            m_player.MeditationEndAnim();
      }
}
