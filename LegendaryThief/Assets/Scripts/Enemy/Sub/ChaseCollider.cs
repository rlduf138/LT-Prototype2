using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCollider : MonoBehaviour
{
      private PoliceBall policeBall;

      void Start()
      {
            policeBall = GetComponentInParent<PoliceBall>();
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            
      }

      private void OnTriggerExit2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  policeBall.EndChase();
            }
      }
}
