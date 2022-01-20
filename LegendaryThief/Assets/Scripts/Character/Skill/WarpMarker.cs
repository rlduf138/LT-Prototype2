using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMarker : MonoBehaviour
{
      private CharacterBase characterBase;
      float distance;

      public float lifetime;
      public float maxDistance;

      void Start()
      {
            if (!characterBase.warpTuto)
            {
                  StartCoroutine(CheckDistance());
                  StartCoroutine(LifeTime());
                  
            }
      }

      public void Setting(CharacterBase _characterBase)
      {
            characterBase = _characterBase;
      }
      public IEnumerator CheckDistance()
      {
            while (true)
            {
                  yield return new WaitForFixedUpdate();
                  distance = Vector2.Distance(characterBase.transform.position, transform.position);

                  //float dis = distance.normalized;
                  if (distance > maxDistance)
                  {
                        Destroy(gameObject);
                  }
            }
      }

      public IEnumerator LifeTime()
      {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
      }
}
