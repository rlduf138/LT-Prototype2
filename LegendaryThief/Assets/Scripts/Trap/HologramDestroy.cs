using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramDestroy : MonoBehaviour
{


      void Start()
      {

      }

      void Update()
      {

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Hologram hologram = collision.GetComponent<Hologram>();
                  if(hologram != null)
                  {
                        hologram.OnDamage(1, transform.position);
                  }
            }
      }
}
