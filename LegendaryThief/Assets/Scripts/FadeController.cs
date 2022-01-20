using PixelSilo.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : Singleton<FadeController>
{
      public Image fadeImg;
      public Image changeImg;
      public Sprite[] changeList;
      public float sceneSpeed;
      public float fadeInTime;
      public float fadeOutTime;
     //////// public OptionController menuObject;
      public void FadeIn( System.Action nextEvent = null)
      {
            StartCoroutine(CoFadeIn( nextEvent));
      }
      public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
      {
            StartCoroutine(CoFadeOut( nextEvent));
      }
      public void OpenScene( System.Action nextEvent = null)
      {
            StartCoroutine(CoOpenScene(  nextEvent));
      }

      public void TitleFadeIn(System.Action nextEvent = null)
      {
            StartCoroutine(NoChFadeIn(nextEvent));
      }
      public void TitleFadeOut(System.Action nextEvent = null)
      {
            StartCoroutine(CoFadeOut( nextEvent));
      }


      public void InitColor(Color color)
      {
            fadeImg.color = color;
      }

      IEnumerator CoFadeIn( System.Action nextEvent = null)
      {
            // 화면 까매지는거
        //    CharacterBase characterBase = FindObjectOfType<CharacterBase>();
         //   characterBase.isTuto = true;  // 캐릭터 조작 막기

            OptionController menuObject = FindObjectOfType<OptionController>();
            Debug.Log("Menu off");
            ////////menuObject.canUse = false;    // 메뉴 조작 막기
            fadeImg.gameObject.SetActive(true);

            //SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            Image sr = fadeImg;
            Color tempColor = sr.color;
            tempColor.a = 0f;
            while (tempColor.a < 1f)
            {
                  tempColor.a += Time.deltaTime / fadeInTime;
                  sr.color = tempColor;

                  if (tempColor.a >= 1f) tempColor.a = 1f;

                  yield return null;
            }
            
            sr.color = tempColor;
           // fadeImg.gameObject.SetActive(false);
            //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            if (nextEvent != null) nextEvent();
      }
      IEnumerator NoChFadeIn(System.Action nextEvent = null)
      {
            fadeImg.gameObject.SetActive(true);

            //SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
            Image sr = fadeImg;
            Color tempColor = sr.color;
            while (tempColor.a < 1f)
            {
                  tempColor.a += Time.deltaTime / fadeInTime;
                  sr.color = tempColor;

                  if (tempColor.a >= 1f) tempColor.a = 1f;

                  yield return null;
            }

            sr.color = tempColor;

            if (nextEvent != null) nextEvent();
      }

      IEnumerator CoFadeOut( System.Action nextEvent = null)
      {
            fadeImg.gameObject.SetActive(true);
            Image sr = fadeImg;
            Color tempColor = sr.color;
            while (tempColor.a > 0f)
            {
                  tempColor.a -= Time.deltaTime / fadeOutTime;
                  sr.color = tempColor;

                  if (tempColor.a <= 0f) tempColor.a = 0f;

                  yield return null;
            }

            sr.color = tempColor;
            if (nextEvent != null) nextEvent();
            fadeImg.gameObject.SetActive(false) ;
      }

      IEnumerator CoOpenScene( System.Action nextEvent = null)
      {
            // 화면 밝아지는거
            changeImg.gameObject.SetActive(true);
            fadeImg.gameObject.SetActive(false);
            changeImg.sprite = changeList[0];

           // CharacterBase characterBase = FindObjectOfType<CharacterBase>();
         //   characterBase.isTuto = true;  // 캐릭터 조작 막기

       //////////     menuObject = FindObjectOfType<OptionController>();
            Debug.Log("Menu Off");
        //////////    menuObject.canUse = false;

            for (int i = 0; i<changeList.Length; i++)
            {
                  yield return new WaitForSeconds(sceneSpeed);
                  changeImg.sprite = changeList[i];
            }

            Debug.Log("Menu On");
        /////////    menuObject.canUse = true;     // 메뉴 조작 풀기
        //    characterBase.isTuto = false; // 캐릭터 조작 풀기

            changeImg.gameObject.SetActive(false);

            if (nextEvent != null) nextEvent();
            
      }

      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }
}
