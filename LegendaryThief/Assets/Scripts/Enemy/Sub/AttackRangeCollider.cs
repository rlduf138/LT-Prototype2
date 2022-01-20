using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
      private PoliceBall policeBall;

      void Start()
      {
            policeBall = GetComponentInParent<PoliceBall>();
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            
            if (collision.CompareTag("Player"))
            {
                  
                  policeBall.AttackRangeStart();
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if(collision.CompareTag("Player"))
            {
                  policeBall.AttackRangeEnd();
            }
      }
}
