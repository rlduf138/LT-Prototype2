using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
      public bool isActive;
      public float speed;
      Vector2 startPos;
      Vector2 endPos;

      public float posY;
      public float posX;
      public float endX;
      //x -17, 17
      //y -6~7

      protected void Start()
      {
            StartCoroutine("Set");
      }
      public IEnumerator Set()
      {
            float ran = Random.Range(1f, 5f);
            yield return new WaitForSeconds(ran);
            // 위치 배정. 속도 배정
            gameObject.SetActive(true);


            startPos = new Vector2(posX, posY);
            endPos = new Vector2(endX, posY);
            //Flip(true);
            while (true)
            {
                  transform.position = startPos;

                  isActive = true;

                  float ran2 = Random.Range(6f, 9f);
                  yield return new WaitForSeconds(ran2);
                 // gameObject.SetActive(false);
                  // isActive = false;
                  // transform.position = startPos;
            }
      }

      protected void Update()
      {
            if (isActive == true)
            {
                  //Debug.Log("Start : " + startPos + "   end : " + endPos);
                  transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            }
      }

      protected void Flip(bool bLeft)
      {
            transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
      }

}
