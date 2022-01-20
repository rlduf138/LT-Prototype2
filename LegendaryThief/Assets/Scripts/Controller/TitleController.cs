using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
      private BGMController bgmController;
      public GameObject bgmPrefab;

      private void Awake()
      {
         /*   bgmController = FindObjectOfType<BGMController>();
            if (bgmController == null)
            {
                  GameObject obj;
                  obj = Instantiate(bgmPrefab);
                  bgmController = obj.GetComponent<BGMController>();
            }*/

            GameObject checker = GameObject.FindGameObjectWithTag("Menu");
            if (checker != null)
            {
                  Debug.Log("Destroy MenuCanvas");
                  Destroy(checker);
            }
      }
      void Start()
      {
            FadeController.Instance.TitleFadeOut();
          //  bgmController.ChangeTitleBGM();
      }

      void Update()
      {
            if (Input.anyKeyDown)
            {
                  NextScene(); ;
            }
      }

      private void NextScene()
      {
            FadeController.Instance.TitleFadeIn(ActiveNextScene);
      }
      private void ActiveNextScene()
      {

            /*if(!PlayerPrefs.HasKey("Tutorial"))
            {
                  UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("Tutorial");
            }
            else if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                  UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("Agit");
            }else if(PlayerPrefs.GetInt("Tutorial") == 0)
            {
                  UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("Tutorial");
            }*/
            UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("NewAgit");
      }
}
