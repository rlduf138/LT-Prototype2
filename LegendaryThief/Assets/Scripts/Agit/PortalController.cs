using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : Highlight
{
      Action next;
      private bool isShaderActive;

      public GameObject particleObject;

      // Update is called once per frame
      void Update()
      {
            if (Input.GetKeyUp(KeyCode.F) && isActive)
            {
                  if (!isShaderActive)
                  {
                        StartCoroutine(ActiveShader(next));
                  }
            }
      }


      public IEnumerator ActiveShader(Action _next)
      {
            isShaderActive = true;
            CharacterBase characterBase = FindObjectOfType<CharacterBase>();
            characterBase.isTuto = true;

           // Instantiate(goWarp)

            yield return new WaitForSeconds(1f);

      }
}
