using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChArrow : MonoBehaviour
{
      public GameObject arrow;
      public float time;
      public float speed;
      private Vector3 direction;
      private Vector3 direction01;
      private float currentTime;

      void Start()
      {
            StartCoroutine(Move());
      }

      public IEnumerator Move()
      {
            direction = new Vector3(0, 1, 0);
            direction.Normalize();
            direction01 = new Vector3(0, -1, 0);
            direction01.Normalize();

            currentTime = 0f;

            while (true)
            {
                  yield return new WaitForFixedUpdate();
                  if (currentTime < time / 2)
                  {
                        transform.Translate(direction * speed * Time.deltaTime, Space.World);
                  }else if( currentTime <= time)
                  {
                        transform.Translate(direction01 * speed * Time.deltaTime, Space.World);
                  }
                  currentTime += Time.deltaTime;
                  if(currentTime > time)
                  {
                        currentTime = 0;
                  }
            }

      }
}
