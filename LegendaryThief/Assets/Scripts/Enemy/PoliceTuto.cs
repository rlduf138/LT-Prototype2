using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceTuto : MonoBehaviour
{
      public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
      public Animator m_anim;
      public Transform firMovePosition;
      public Transform secMovePosition;
      public Transform thirMovePosition;
      public void MoveFirst()
      {
            StartCoroutine(Move(firMovePosition));
      }

      public void MoveSecond()
      {
            StartCoroutine(Move(secMovePosition));
      }
      public void MoveThird()
      {
            StartCoroutine(Move(thirMovePosition));
            Destroy(gameObject, 3f);
      }

      public IEnumerator Move(Transform endTransform)
      {
            var t = 0f;
            var start = transform.position;
            var end = endTransform.position;

            m_anim.SetBool("Run", true);

            while (t < 1f)
            {
                  t += Time.deltaTime / 2f;
                  transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  yield return null;
            }
           // m_anim.SetBool("Run", false);

      }

      

      /*public override IEnumerator Patrol()
      {
            Vector3 direction;
            if (!toRight)
            {
                  direction = patrolPos00.position - patrolPos01.position;
                  toRight = true;
            }
            else
            {
                  direction = patrolPos01.position - patrolPos00.position;
                  toRight = false;
            }
            direction.Normalize();

            if (direction.x < 0)
            {
                  Flip(false);
            }
            else
            {
                  Flip(true);
            }
            currentTime = 0f;
            m_anim.SetBool("Run", true);
            while (currentTime < moveTime)
            {

                  yield return new WaitForFixedUpdate();
                  if (!isFind)
                  {
                        transform.Translate(direction * speed * Time.deltaTime, Space.World);
                        currentTime += Time.deltaTime;
                  }
            }
            m_anim.SetBool("Run", false);
            //yield return new WaitForSeconds(patrolTime);
      }*/
}
