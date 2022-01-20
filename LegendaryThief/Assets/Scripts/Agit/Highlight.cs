using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
      public SpriteRenderer spriteRenderer;
      public Sprite non_sprite;
      public Sprite highlight_sprite;
      public bool isActive = false;
      public CharacterBase character;


      protected void Start()
      {
            character = FindObjectOfType<CharacterBase>();
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  spriteRenderer.sprite = highlight_sprite;
                  isActive = true;
            }
      }

      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  spriteRenderer.sprite = non_sprite;
                  isActive = false;
            }
      }


}
