using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapTrigger : MonoBehaviour
{
      private void OnTriggerStay2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("FallingTrapTriggerActive");
                  Player ch =  collision.GetComponent<Player>();
                  if(ch.IsGround())
                  {
                        ch.OnDamage(1f, transform.position);
                        Debug.Log("FallingTrapHit");
                  }
            }
      }
}
