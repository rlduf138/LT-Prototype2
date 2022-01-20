using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour
{
      public int maxStack;
      public int stack;

      private Animator m_animator;
      public HiddenDoorTile hiddenTile;
      // Start is called before the first frame update
      void Start()
      {
            m_animator = GetComponent<Animator>();
            Init();
      }

      public void Init()
      {
            stack = maxStack;

            if (hiddenTile != null)
            {
                  hiddenTile.Reset();
            }
      }

      // Update is called once per frame
      void Update()
      {

      }

      private void OnCollisionEnter2D(Collision2D collision)
      {
            if (collision.gameObject.CompareTag("Player"))
            {
                  Player player = collision.gameObject.GetComponent<Player>();
                //  if (player.m_wallHit)
               //   {
                //        stack--;
                //        StartCoroutine("ShakeDoor");
                //  }
            }
      }

      public IEnumerator ShakeDoor()
      {
            yield return null;
            m_animator.SetTrigger("Shake");

            if (stack <= 0)
            {
                  // 히든 도어 스택이 0이되면.
                  yield return new WaitForSeconds(0.4f);
                  gameObject.SetActive(false);
            }
      }
}
