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
                  Player ch = collision.GetComponent<Player>();
                  Hologram hologram = collision.gameObject.GetComponent<Hologram>();
                  if (ch != null)
                  {
                        if (ch.IsGround())
                        {
                              ch.OnDamage(1f, transform.position);
                              Debug.Log("FallingTrapHit");
                        }
                  }else if(hologram != null)
                  {
                        if (hologram.IsGround())
                        {
                              hologram.OnDamage(1f, transform.position);
                              Debug.Log("FallingTrapHit");
                        }
                  }
            }
      }
}
