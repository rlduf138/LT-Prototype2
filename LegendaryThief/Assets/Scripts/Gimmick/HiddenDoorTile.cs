using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenDoorTile : MonoBehaviour
{
      public Tilemap tileMap;
      public float hiddenTime;
      public float fadeTime;
      void Start()
      {

      }

      void Update()
      {

      }

      public void Reset()
      {
            StopCoroutine("TileOn");
            StopCoroutine("TileRemove");
            Color tempColor = tileMap.color;
            tempColor.a = 1f;
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("TriggerEnter");
                  StopCoroutine("TileOn");
                  StartCoroutine("TileRemove");
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("TriggerExit");
                  StopCoroutine("TileRemove");
                  StartCoroutine("TileOn");
            }
      }

      public IEnumerator TileRemove()
      {
            Color tempColor = tileMap.color;
            while (tempColor.a > 0f)
            {
                  tempColor.a -= Time.deltaTime/fadeTime;
                  tileMap.color = tempColor;
                  if (tempColor.a <= 0f) tempColor.a = 0f;
                  yield return null;
            }
            tileMap.color = tempColor;
      }
      public IEnumerator TileOn()
      {
            Color tempColor = tileMap.color;
            while (tempColor.a < 1f)
            {
                  tempColor.a += Time.deltaTime/fadeTime;
                  tileMap.color = tempColor;
                  if (tempColor.a >= 1f) tempColor.a = 1f;
                  yield return null;
            }
            tileMap.color = tempColor;
      }
}
