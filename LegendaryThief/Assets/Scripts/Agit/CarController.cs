using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

      public List<Car> cars;

      // Start is called before the first frame update
      void Start()
      {
            for (int i = 0; i < cars.Count; i++)
            {
                  cars[i].gameObject.SetActive(false);
            }
            StartCoroutine("CarActive");
      }

      // Update is called once per frame
      void Update()
      {

      }

      public IEnumerator CarActive()
      {
            while (true)
            {
                  float time = Random.Range(4f, 8f);
                  yield return new WaitForSeconds(time);

                  int ran = Random.Range(0, cars.Count);
                  if(cars[ran].isActive == true)
                  {
                        ran++;
                  }
                  cars[ran].gameObject.SetActive(true);
                  cars[ran].StartCoroutine("Set");
            }
      }
}
