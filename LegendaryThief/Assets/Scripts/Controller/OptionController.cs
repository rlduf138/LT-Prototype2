using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
      public GameObject menuCanvas;
      public bool isMenu;
      public GameObject optionPanel;
      public bool isOption;
      private AudioMixerSlider audioMixerSlider;
      public bool canUse = true;
      public GameObject skipButton;
      // Start is called before the first frame update
      void Start()
      {
            audioMixerSlider = GetComponent<AudioMixerSlider>();
      }

      // Update is called once per frame
      void Update()
      {

            if (canUse && Input.GetKeyUp(KeyCode.Escape))
            {
                  if (!isMenu)
                  {
                        // 메뉴 호출
                        menuCanvas.SetActive(true);
                        isMenu = true;
                        Time.timeScale = 0f;

                        Player player = FindObjectOfType<Player>();
                        player.m_canControl = false;
                        //characterBase.isTuto = true;
                  }
                  else if (isMenu)
                  {
                         CloseMenu();
                  }
                  // 튜토스킵버튼 활성화
                  /*   if (PlayerPrefs.HasKey("Tutorial"))
                     {
                           if (PlayerPrefs.GetInt("Tutorial") == 1)
                           {
                                 skipButton.SetActive(false);
                           }
                           else
                           {
                                 skipButton.SetActive(true);
                           }
                     }
                     else
                     {
                           skipButton.SetActive(true);
                     }

                     if (!isMenu)
                     {
                           // 메뉴 호출
                           menuCanvas.SetActive(true);
                           isMenu = true;
                           Time.timeScale = 0f;

                           CharacterBase characterBase = FindObjectOfType<CharacterBase>();
                           //characterBase.isTuto = true;
                     }
                     else if (isMenu)
                     {
                           // 메뉴창이 켜져있을때
                           if (isOption)
                           {
                                 optionPanel.SetActive(false);
                                 audioMixerSlider.SaveOption();
                                 isOption = false;
                           }
                           else if (!isOption)
                           {
                                 // 옵션창 꺼져있을때.
                                 CloseMenu();

                           }
                     }*/
            }
            }
      public void CloseMenu()
      {
            menuCanvas.SetActive(false);
            isMenu = false;
            Time.timeScale = 1f;
            Player player = FindObjectOfType<Player>();
            player.m_canControl = true;
      }
      public void ExitButtonClick()
      {
            Application.Quit();
      }
      public void OptionButtonClicked()
      {
            optionPanel.SetActive(true);
            isOption = true;
      }

      public void ResetButton()
      {
            if (PlayerPrefs.HasKey("Tutorial"))
            {
                  PlayerPrefs.DeleteKey("Tutorial");
                  CloseMenu();
                  FadeController.Instance.TitleFadeIn(ResetScene);
            }
      }
      private void ResetScene()
      {
           
            UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("Title");
      }

      public void SkipTutorial()
      {
            PlayerPrefs.SetInt("Tutorial", 1);
            CloseMenu();
            FadeController.Instance.TitleFadeIn(SkipScene);
      }
      private void SkipScene()
      {
            UnityEngine.SceneManagement.SceneManager
                        .LoadSceneAsync("Agit");
      }
}
