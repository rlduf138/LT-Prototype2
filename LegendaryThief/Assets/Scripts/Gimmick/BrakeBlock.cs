using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeBlock : MonoBehaviour
{
      public float brakeTime = 2f;
      public float respawnTime = 5f;
      public float fadeTime = 0.5f;
      public Color[] temps;
      public BoxCollider2D coll;
    //  public SpriteRenderer[] sprites;
      public Animator m_animator;
      void Awake()
      {
            

      }

      void Update()
      {

      }

      public void ResetBlock()
      {
            StopCoroutine("Brake");
            StopCoroutine("Respawn");
            coll.enabled = true;
            /*   for(int i = 0; i<sprites.Length; i++)
               {
                     Color temp = sprites[i].color;
                     temp.a = 1f;
                     sprites[i].color = temp;
               }*/
            m_animator.SetBool("Brake", false);
      }

      private void OnCollisionEnter2D(Collision2D collision)
      {
            if (collision.gameObject.CompareTag("Player"))
            {
                  StartCoroutine("Brake");
            }    
      }

      private IEnumerator Brake()
      {
            m_animator.SetBool("Brake", true);

            yield return new WaitForSeconds(brakeTime - 0.5f);

            coll.enabled = false;
            //Color[] temps = ;
            /*for(int i = 0; i<sprites.Length; i++)
            {
                  temps[i] = sprites[i].color;
            }
            
            while (temps[0].a > 0f)
            {
                  for(int i = 0; i<sprites.Length; i++)
                  {
                        temps[i].a -= Time.deltaTime / fadeTime;
                        sprites[i].color = temps[i];
                        if (temps[i].a <= 0f) temps[i].a = 0f;
                  }
                  yield return null;
            }
            
            for(int i = 0; i<sprites.Length; i++)
            {
                  sprites[i].color = temps[i];
            }

            coll.enabled = false;*/
            StartCoroutine("Respawn");
      }

      private IEnumerator Respawn()
      {
            yield return new WaitForSeconds(respawnTime - 0.5f);
            //Color[] temps = new Color;
            /*for (int i = 0; i < sprites.Length; i++)
            {
                  temps[i] = sprites[i].color;
            }

            while (temps[0].a < 1f)
            {
                  for (int i = 0; i < sprites.Length; i++)
                  {
                        temps[i].a += Time.deltaTime / fadeTime;
                        sprites[i].color = temps[i];
                        if (temps[i].a >= 1f) temps[i].a = 1f;
                  }
                  yield return null;
            }

            for (int i = 0; i < sprites.Length; i++)
            {
                  sprites[i].color = temps[i];
            }*/
            m_animator.SetBool("Brake", false);
            coll.enabled = true;
      }
}
