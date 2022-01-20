using UnityEngine;
using System.Collections;

public class Sensor_Prototype : MonoBehaviour
{

      public int m_ColCount = 0;
      public int m_WallCount = 0;
      public float m_DisableTimer;
      public bool m_ground;

      private void OnEnable()
      {
            m_ColCount = 0;
      }

      public bool State()
      {
            if (m_DisableTimer > 0)
                  return false;
            return m_ColCount > 0;
      }

      public bool WallState()
      {
            if (m_DisableTimer > 0)
                  return false;
            return m_WallCount > 0;
      }

      void OnTriggerEnter2D(Collider2D other)
      {
            if (!m_ground)
            {
                  if (other.CompareTag("Ground"))
                  {
                        m_ColCount++;
                  }else if (other.CompareTag("Wall"))
                  {
                        m_WallCount++;
                  }
            }
            else
            {
                  if (other.CompareTag("Wall") || other.CompareTag("Ground"))
                  {
                        m_ColCount++;
                  }
            }
      }

      void OnTriggerExit2D(Collider2D other)
      {
            if (!m_ground)
            {
                  if (other.CompareTag("Ground"))
                  {
                        m_ColCount--;
                  }
                  else if (other.CompareTag("Wall"))
                  {
                        m_WallCount--;
                  }
            }
            else
            {
                  if (other.CompareTag("Wall") || other.CompareTag("Ground"))
                  {
                        m_ColCount--;
                  }
            }
      }

      void Update()
      {
            m_DisableTimer -= Time.deltaTime;
      }

      public void Disable(float duration)
      {
            //Debug.Log("Disable + " + duration);
            m_DisableTimer = duration;
      }
}
