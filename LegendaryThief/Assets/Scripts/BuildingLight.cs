using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BuildingLight : MonoBehaviour
{
      public float intenMax;
      public float intenMin;
      public float speed;
      public Light2D _light;

      private bool islightMax;

      protected void Start()
      {
            _light = GetComponent<Light2D>();
            StartCoroutine(LightOn());
      }

      public IEnumerator LightOn()
      {
          //  float ran = Random.Range(ran_min, ran_max);
        //    yield return new WaitForSeconds(ran);

            while (true)
            {
                  yield return new WaitForFixedUpdate();
                  if(_light.intensity >= intenMax)
                  {
                        islightMax = true;
                  }else if(_light.intensity <= intenMin)
                  {
                        islightMax = false;
                  }
                  if (islightMax)
                  {
                        _light.intensity -= Time.deltaTime / speed;
                  }
                  else
                  {
                        _light.intensity += Time.deltaTime / speed;
                  }
            }
      }
}
