using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
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
                  Debug.Log("AttackCOllider");
               //   policeBall.AttackRangeStart();
                  collision.GetComponent<CharacterBase>().OnDamage(1f, transform.position);
            }
      }
      
}
