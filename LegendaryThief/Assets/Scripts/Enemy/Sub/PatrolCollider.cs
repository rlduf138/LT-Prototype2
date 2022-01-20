using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollider : MonoBehaviour
{
      private PoliceBall policeBall;

      void Start()
      {
            policeBall = GetComponentInParent<PoliceBall>();
      }

      void Update()
      {

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  CharacterBase cBase = collision.GetComponent<CharacterBase>();
                  policeBall.ReadyToChase(cBase);
            }
      }
      private void OnTriggerExit(Collider other)
      {
            
      }
}
