using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
      public Transform leftPosition;
      public Transform rightPosition;
      public Transform originPosition;
      public bool oneTimeActive;    // 한번만 작동.
      private bool isOneTimeActive;
      private BoxCollider2D coll;
      public float speed = 2f;
      public float time = 3f;

      public bool isLeft;
      public bool isRight;


      // Start is called before the first frame update
      void Start()
      {
              
      }

      // Update is called once per frame
   

      public IEnumerator LeftToRightMove()
      {
            isLeft = false;
            var t = 0f;
            var start = leftPosition.position;
            var end = rightPosition.position;
            AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

            while (t < time)
            {
                  yield return new WaitForFixedUpdate();
                  // t += Time.deltaTime / speed;
                  //transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  t += Time.deltaTime;
                  // var dest = end - start;
                  //  transform.Translate(dest * speed * Time.deltaTime, Space.World);
                  transform.position = Vector3.MoveTowards(transform.position, end, speed);
                  
                  yield return null;
            }
            isRight = true;
      }
      public IEnumerator RightToLeftMove()
      {
            isRight = false;
            var t = 0f;
            var start = rightPosition.position;
            var end = leftPosition.position;
            AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

            while (t < time)
            {
             
                  yield return new WaitForFixedUpdate();
                  //    t += Time.deltaTime / speed;
                  // transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  t += Time.deltaTime;
                  //  var dest = start - end;
                  // transform.Translate(dest * speed * Time.deltaTime, Space.World);
                  transform.position = Vector3.MoveTowards(transform.position, end, speed);

                  yield return null;
            }
            isLeft = true;
      }
    /*  private void OnCollisionEnter2D(Collision2D collision)
      {
            if (collision.gameObject.CompareTag("Player"))
            {
                  if (isLeft)
                  {
                        StartCoroutine("LeftToRightMove");
                  }else if (isRight)
                  {
                        StartCoroutine("RightToLeftMove");
                  }
            }
      }*/

      public void ResetPosition()
      {
            StopAllCoroutines();
            isOneTimeActive = false;
                 isLeft = true;
            transform.position = originPosition.position;
            
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("부모 설정");
                  collision.transform.parent = transform;
                  if (oneTimeActive &&  !isOneTimeActive)
                  {
                        if (isLeft)
                        {
                              StartCoroutine("LeftToRightMove");
                              isOneTimeActive = true;
                        }
                        else if (isRight)
                        {
                              StartCoroutine("RightToLeftMove");
                              isOneTimeActive = true;
                        }
                  }
                  else if(!oneTimeActive)
                  {
                        if (isLeft)
                        {
                              StartCoroutine("LeftToRightMove");
                           
                        }
                        else if (isRight)
                        {
                              StartCoroutine("RightToLeftMove");
                            
                        }
                  }
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Debug.Log("부모 설정 해제");
                  collision.transform.parent = null;
            }
      }
}
