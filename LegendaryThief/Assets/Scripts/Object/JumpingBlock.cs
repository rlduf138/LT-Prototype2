using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBlock : MonoBehaviour
{
      public float yPower;
      BoxCollider2D coll;

      // Start is called before the first frame update
      void Start()
      {
            coll = GetComponent<BoxCollider2D>();
      }

      // Update is called once per frame
      void Update()
      {

      }
      private void OnCollisionEnter2D(Collision2D collision)
      {
            
            if (collision.gameObject.CompareTag("Player"))
            {
                  Debug.Log("Active JumpingBlock");
                  Player player = collision.gameObject.GetComponent<Player>();
                  float yVec;
                  if (player.m_body2d.velocity.y < 0)
                  {
                        yVec = yPower;
                  }
                  else
                  {
                        yVec = player.m_body2d.velocity.y + yPower;
                  }
                  player.m_body2d.velocity = new Vector2(player.m_body2d.velocity.x, yVec);
                  player.m_animator.SetTrigger("Jump");
                  coll.enabled = false;
                  StartCoroutine("ColliderOn");
            }
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("Active JumpingBlock");
                  Player player = collision.GetComponent<Player>();
                  float yVec;
                  if(player.m_body2d.velocity.y < 0)
                  {
                        yVec = yPower;
                  }
                  else
                  {
                        yVec = player.m_body2d.velocity.y + yPower;
                  }
                  player.m_body2d.velocity = new Vector2(player.m_body2d.velocity.x, yVec);
                  coll.enabled = false;
                  StartCoroutine("ColliderOn");
            }
      }

      private IEnumerator ColliderOn()
      {
            yield return new WaitForSeconds(0.3f);
            coll.enabled = true;
      }
}
