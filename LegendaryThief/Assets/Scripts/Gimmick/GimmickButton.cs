using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickButton : MonoBehaviour
{
      public List<ActiveObject> activeList;
      public Player player;
      public Hologram hologram;
      public BoxCollider2D coll;

      public Animator m_animator;
      public bool isActive;
      private float m_ActiveCoolTime;     // 작동 쿨타임.
      public float coolTime = 1f;
      //     public GameObject activeMark;

      public GameObject talkBubble;

      // Start is called before the first frame update
      protected void Start()
      {

            // player = FindObjectOfType<Player>();
      }
      private void OnEnable()
      {
            m_ActiveCoolTime = 0f;
            m_animator.SetBool("Active", true);
      }
      // Update is called once per frame
      protected void Update()
      {
            if (Input.GetKeyDown("f"))
            {
                  if (m_ActiveCoolTime <= 0)    // 작동 쿨타임.
                  {
                        Debug.Log("Input Key F");
                        if (player != null || hologram != null)
                        {
                              if (!isActive)
                              {
                                    Debug.Log("CheckDeerg");
                                    CheckDeerg();
                              }

                        }
                        else if (player == null && hologram == null)
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
                  hologram = collision.gameObject.GetComponent<Hologram>();
                  if (talkBubble != null)
                  {
                        talkBubble.SetActive(true);
                  }

            }


      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  player = null;
                  if (talkBubble != null)
                  {
                        talkBubble.SetActive(false);
                  }
            }


      }

      public void CheckDeerg()
      {
            for(int i = 0; i < activeList.Count; i++)
            {
                  activeList[i].Active();
            }

            Debug.Log("Deerg -1");
            isActive = true;
            m_animator.SetBool("Active", !isActive);
            //     activeMark.SetActive(true);

            m_ActiveCoolTime = coolTime;
            Destroy(talkBubble);
      }




}
