using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMovePlatform : ActiveObject
{
      public Transform leftPosition;
      public Transform rightPosition;
      public Transform originPosition;

      public float speed = 1f;
      public float time = 1f;

      public bool isLeft;
      public bool isRight;

      [Header("OriginStartPos")]
      public bool firstLeft;        // 리셋 위치 왼쪽
      public bool firstRight;       // 리셋 위치 오른쪽

      void Start()
      {

      }

      void Update()
      {

      }
      public override void InitActive()
      {
            if (firstLeft)
            {
                  transform.position = rightPosition.position;
                  isRight = true;
                  isLeft = false;
            }
            else
            {
                  transform.position = leftPosition.position;
                  isLeft = true;
                  isRight = false;
            }
      }
      public override void Active()
      {
          
                  if (firstLeft)
                  {
                        StartCoroutine("LeftToRightMove");
                  }
                  else if (firstRight)
                  {
                        StartCoroutine("RightToLeftMove");
                  }
            
      }

      public override void DeActive()
      {
            StopAllCoroutines();
            if (firstLeft)
            {
                  StartCoroutine("RightToLeftMove");
            }else if (firstRight)
            {
                  StartCoroutine("LeftToRightMove");
            }
      }
      public override void ResetActiveObject()
      {
            ResetPosition();
      }
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
                  t += Time.deltaTime / 1f;
                  transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  
                  //var dest = end - start;
                //  transform.Translate(dest * speed * Time.deltaTime, Space.World);
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
                  t += Time.deltaTime / 1f;
                  transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                 // t += Time.deltaTime;
                //  var dest = start - end;
                 // transform.Translate(dest * speed * Time.deltaTime, Space.World);
                  yield return null;
            }
            isLeft = true;
      }
      public void ResetPosition()
      {
            StopAllCoroutines();
            
            transform.position = originPosition.position;

      }
}
