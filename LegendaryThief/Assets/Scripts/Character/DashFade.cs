using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFade : MonoBehaviour
{
      public SpriteRenderer sprite;
      // Start is called before the first frame update
      void Start()
      {
            StartCoroutine("DashDisapear");
      }

      // Update is called once per frame
      void Update()
      {

      }

      public IEnumerator DashDisapear()
      {
            Color temp = sprite.color;

            while (temp.a > 0f)
            {

                  temp.a -= Time.deltaTime/0.5f;
                  sprite.color = temp;

                  yield return null;
            }

            Destroy(gameObject);
      }
}
