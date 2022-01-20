using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMachine : MonoBehaviour
{
      public ActiveObject activeObject;
      public Player player;
      public BoxCollider2D coll;
      public Deerg deerg;
      public Animator m_animator;
      public bool isActive;
      public bool isPass = false;   // 다음맵 넘어가서 기믹 확정되면 true
      private float m_ActiveCoolTime;     // 작동 쿨타임.
      public float coolTime = 1f;
 //     public GameObject activeMark;

      // Start is called before the first frame update
      protected void Start()
      {
            // player = FindObjectOfType<Player>();
      }
      private void OnEnable()
      {
            m_ActiveCoolTime = 0f;
      }
      // Update is called once per frame
      protected void Update()
      {
            if (Input.GetKeyDown("f"))
            {
                  if (m_ActiveCoolTime <= 0)    // 작동 쿨타임.
                  {
                        Debug.Log("Input Key F");
                        if (player != null)
                        {
                              if (!isActive)
                              {
                                    Debug.Log("CheckDeerg");
                                    CheckDeerg();
                              }
                              else if (isActive)
                              {
                                    OutDeerg();
                              }
                        }
                        else if (player == null)
                        {
                              Debug.Log("Player null");
                        }
                  }
            }
            m_ActiveCoolTime -= Time.deltaTime;
      }

      
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  player = collision.GetComponent<Player>();
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  player = null;
            }
      }

      public void CheckDeerg()
      {
            if (player.deergs.Count >= 1)
            {
                
                  deerg = player.deergs[0];
                //  deerg.isReset = false;
                  player.deergs.RemoveAt(0);

                  activeObject.Active();
                  Debug.Log("Deerg -1");
                  isActive = true;
                  m_animator.SetBool("Active", isActive);
                  //     activeMark.SetActive(true);

                  m_ActiveCoolTime = coolTime;
            }
            else
            {
                  Debug.Log("Deerg 부족");
                  return;
            }
      }

      public void InitFuelMachine()
      {
            deerg = null;
            //activeObject.DeActive();
            activeObject.ResetActiveObject();
            isActive = false;
            m_animator.SetBool("Active", isActive);
      //      activeMark.SetActive(false);
      }

      public void OutDeerg()
      {
            Debug.Log("OutDeerg");
            deerg.isReset = true;
            player.deergs.Add(deerg);

            deerg = null;
            activeObject.DeActive();

            isActive = false;
            m_animator.SetBool("Active", isActive);
            //     activeMark.SetActive(false);
            m_ActiveCoolTime = coolTime;
      }

}
