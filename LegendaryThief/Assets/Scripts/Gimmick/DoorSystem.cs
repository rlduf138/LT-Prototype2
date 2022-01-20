using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : ActiveObject
{
      public GameObject leftDoor;
      public GameObject rightDoor;
      public Transform leftDoorOrigin;
      public Transform rightDoorOrigin;
      public Transform leftDoorDest;
      public Transform rightDoorDest;

      public override void Active()
      {
            if (!isActive)
            {
                  StopCoroutine("CloseDoor");
                  StartCoroutine("OpenDoor");
                  isActive = true;
            }
            else
            {
                  // 이미 작동되있는 문 스테이지 다시 돌아올때.
                  leftDoor.transform.position = leftDoorDest.position;
                  rightDoor.transform.position = rightDoorDest.position;
            }
      }
      public override void DeActive()
      {
            if (isActive)
            {
                  StopCoroutine("OpenDoor");
                  StartCoroutine("CloseDoor");
                  isActive = false;
            }
            else
            {
                  leftDoor.transform.position = leftDoorOrigin.position;
                  rightDoor.transform.position = rightDoorOrigin.position;
            }
      }

      public IEnumerator OpenDoor()
      {
            var t = 0f;
            AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
            while (t < 1f)
            {
                  // m_animator.SetInteger("AnimState", 1);
                  t += Time.deltaTime / 1f;
                  leftDoor.transform.position = Vector3.LerpUnclamped(leftDoorOrigin.position, leftDoorDest.position, curve.Evaluate(t));
                  rightDoor.transform.position = Vector3.LerpUnclamped(rightDoorOrigin.position, rightDoorDest.position, curve.Evaluate(t));
                  yield return null;
            }
      }
      public IEnumerator CloseDoor()
      {
            var t = 0f;
            AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
            while (t < 1f)
            {
                  // m_animator.SetInteger("AnimState", 1);
                  t += Time.deltaTime / 1f;
                  leftDoor.transform.position = Vector3.LerpUnclamped(leftDoorOrigin.position, leftDoorOrigin.position, curve.Evaluate(t));
                  rightDoor.transform.position = Vector3.LerpUnclamped(rightDoorOrigin.position, rightDoorOrigin.position, curve.Evaluate(t));
                  yield return null;
            }
      }
}
