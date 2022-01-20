using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{

      public GameObject trapObject;

      public float trapOriginX;
      public float trapOriginY;

      public int shakeCount;
      public float shakeGapTime;
      public float shakeX;
      public float shakeY;

      public float trapGravity;

      public bool isTrapActive;

      public GameObject holder01;
      public GameObject holder02;
      private Animator holder01Anim;
      private Animator holder02Anim;

      // Start is called before the first frame update
      void Start()
      {
            holder01Anim = holder01.GetComponent<Animator>();
            holder02Anim = holder02.GetComponent<Animator>();
      }

      // Update is called once per frame
      void Update()
      {

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  if (isTrapActive == false)
                  {
                        StartCoroutine("TrapActive");
                  }
            }
      }
      public void ResetTrap() {
            StopCoroutine("TrapActive");
            trapObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            trapObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            trapObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            trapObject.transform.position = new Vector2(trapOriginX, trapOriginY);
            isTrapActive = false;
      }

      public IEnumerator TrapActive()
      {
            isTrapActive = true;
            int count = 0;
            trapObject.transform.position = new Vector2(trapObject.transform.position.x + shakeX, trapObject.transform.position.y + shakeY);
            yield return new WaitForSeconds(0.1f);
            
            while (shakeCount > count)
            {
                  count++;
                  trapObject.transform.position = new Vector2(trapObject.transform.position.x - shakeX * 2, trapObject.transform.position.y - shakeY * 2);
                  yield return new WaitForSeconds(shakeGapTime);
                  trapObject.transform.position = new Vector2(trapObject.transform.position.x + shakeX * 2, trapObject.transform.position.y + shakeY * 2);
                  yield return new WaitForSeconds(shakeGapTime);

            }
            trapObject.transform.position = new Vector2(trapOriginX, trapOriginY);
            yield return new WaitForSeconds(shakeGapTime);
           /// holder01Anim.SetTrigger("Active");
           // holder02Anim.SetTrigger("Active");
            trapObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            trapObject.GetComponent<Rigidbody2D>().gravityScale = trapGravity;
      }
}
