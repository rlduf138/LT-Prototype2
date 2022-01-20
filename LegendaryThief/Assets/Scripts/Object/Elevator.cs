using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
      public Transform upTransform;
      public Transform downTransform;
      public Transform originTransform;

      public float speed;

      public bool isUp = false;       // 위에 있음.
      public bool isDown = true;
      public bool isMove = false;

      // Start is called before the first frame update
      void Start()
      {
            transform.position = originTransform.position;
      }

      // Update is called once per frame
      void Update()
      {

      }


      public IEnumerator MoveUp()
      {
            Debug.Log("MoveUp");
            yield return new WaitForSeconds(1f);
            
            isMove = true;
            isUp = false;
            isDown = false;

            bool checker = true;
            while (checker)
            {
                  transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);

                  if(transform.position.y >= upTransform.position.y)
                  {
                        checker = false;
                  }

                  yield return new WaitForFixedUpdate();
            }

            isUp = true;
            transform.position = upTransform.position;
      }
      public IEnumerator MoveDown()
      {
            Debug.Log("MoveDown");
            yield return new WaitForSeconds(1f);

            isMove = true;
            isUp = false;
            isDown = false;

            bool checker = true;
            while (checker)
            {
                  transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);

                  if (transform.position.y <= downTransform.position.y)
                  {
                        checker = false;
                  }

                  yield return new WaitForFixedUpdate();
            }
            isDown = true;
            transform.position = downTransform.position;
      }

      public IEnumerator ResetTimer()
      {
            yield return new WaitForSeconds(5f);
            StartCoroutine("MoveDown");
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  collision.transform.parent = transform;
                  if (isDown)
                  {
                        StartCoroutine("MoveUp");
                  }else if (isUp)
                  {
                        StartCoroutine("MoveDown");
                  }
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  collision.transform.parent = null;
            }
      }

}
