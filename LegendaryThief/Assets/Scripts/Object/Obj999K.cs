using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obj999K : MonoBehaviour
{

      public GameObject talkBubble;
      Animator m_animator;

      // Start is called before the first frame update
      void Start()
      {
            m_animator = GetComponent<Animator>();
      }

      // Update is called once per frame
      void Update()
      {

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  m_animator.SetBool("Active", true);

            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  m_animator.SetBool("Active", false);
                  talkBubble.SetActive(false);
            }
      }

      public void ActiveBubble()
      {
            Debug.Log("ActiveBubble");
            talkBubble.SetActive(true);
      }
      public void DeActiveBubble()
      {
            Debug.Log("DeActiveBubble");
            talkBubble.SetActive(false);
      }
}
