using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
      public Animator m_animator;
      public bool isActive = false;

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  if (!isActive)
                  {
                        isActive = true;
                        collision.GetComponent<Player>().JewelAdd();
                        StartCoroutine(GetJewel());
                  }
            }
      }

      private IEnumerator GetJewel()
      {
            m_animator.SetTrigger("Get");
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
      }
}
