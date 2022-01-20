using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : ActiveObject
{
      public Animator ventAnim;
      public Animator windAnim;
      public BoxCollider2D trigger;
      public float windForce = 40f;

      public bool windUp = true;
      public bool windDown;
      public bool windRight;
      public bool windLeft;

      int x = 0, y = 0;
      void Start()
      {
            if (windUp)
                  y = 1;
            else if (windDown)
                  y = -1;
            if (windRight)
                  x = 1;
            else if (windLeft)
                  x = -1;
      }

      void Update()
      {

      }

      public override void Active()
      {
            trigger.enabled = true;
            isActive = true;
            ventAnim.SetBool("Active", true);
         //   windAnim.SetBool("Active", true);
      }
      public override void DeActive()
      {
            trigger.enabled = false;
            isActive = false;
            ventAnim.SetBool("Active", false);
         //   windAnim.SetBool("Active", false);
      }
      protected void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  
                  Player player = collision.GetComponent<Player>();
                  if (windUp || windDown)
                        player.m_body2d.velocity = new Vector2(player.m_body2d.velocity.x, windForce * y);
                  else if (windRight || windLeft)
                        player.m_body2d.velocity = new Vector2(windForce * x, player.m_body2d.velocity.y);
            }
      }
}
