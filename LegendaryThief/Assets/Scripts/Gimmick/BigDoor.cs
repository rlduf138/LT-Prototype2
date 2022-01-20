using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
      Animator m_animator;
      public BoxCollider2D coll;
      // Start is called before the first frame update
      void Start()
      {
            m_animator = GetComponent<Animator>();

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  m_animator.SetBool("Active", true);
                //  StopCoroutine("DeActive");
                 // StartCoroutine("Active");
                 
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  m_animator.SetBool("Active", false);
                 // StopCoroutine("Active");
                 // StartCoroutine("DeActive");
            }
      }

      
      public void Open()
      {
            coll.enabled = false;
      }
      public void Close()
      {
            coll.enabled = true;
      }

      public IEnumerator Active()
      {
            yield return new WaitForSeconds(1f);
            coll.enabled = false;
      }
      public IEnumerator DeActive()
      {
            yield return new WaitForSeconds(1f);
            coll.enabled = true;
      }
}
