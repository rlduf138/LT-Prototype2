using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : Highlight
{
      protected Action next;

      public GameObject goWarp;
      public GameObject comeWarp;

      private bool isShaderActive;
      public bool isTutoLock;

      public float particleTime = 1.3f;
      public void SetWarp(Action _next)
      {
            next = _next;
      }

      protected void Update()
      {
            if (Input.GetKeyUp(KeyCode.F) && isActive && !isTutoLock)
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
            characterBase.WarpSound();
            Instantiate(goWarp, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(particleTime);

            characterBase.OffSprite();
            GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(1f);
            //  AgitController.Instance.NextScene();
            PlayerPrefs.SetInt("Warp", 1);
            _next();

      }

      public IEnumerator ComeShader()
      {
            CharacterBase characterBase = FindObjectOfType<CharacterBase>();
            characterBase.isTuto = true;
            characterBase.WarpSound();
            isShaderActive = true;

            Instantiate(comeWarp, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(particleTime);

            characterBase.isTuto = false;
            characterBase.OnSprite();
            GetComponent<SpriteRenderer>().enabled = true;

            yield return new WaitForSeconds(0.3f);
            isShaderActive = false;
            //  AgitController.Instance.NextScene();
            
      }

}
