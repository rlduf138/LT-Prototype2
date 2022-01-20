using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
      private BGMController bgmController;
      public GameObject bgmPrefab;
      private OptionController optionController;
      public GameObject optionPrefab;

      [Header("TalkObject")]
      public Image portrait_img;
      public TextMeshProUGUI talk_text;
      public TextMeshProUGUI name_text;
      public GameObject talkPanel;

      protected void Awake()
      {
            bgmController = FindObjectOfType<BGMController>();
            if(bgmController == null)
            {
                  GameObject obj;
                  obj =  Instantiate(bgmPrefab);
                  bgmController = obj.GetComponent<BGMController>();
            }
            optionController = FindObjectOfType<OptionController>();
            if(optionController == null)
            {
                  GameObject obj;
                  obj = Instantiate(optionPrefab);
                  optionController = obj.GetComponent<OptionController>();
            }
            //FadeController.Instance.OpenScene(0.1f, Active);
      }

      protected void Active()
      {
            CharacterBase characterBase = FindObjectOfType<CharacterBase>();
            characterBase.isTuto = true;  // FadeIn에서 isTuto를 풀어주기에 튜토리얼에서는 다시 막는다.
            
      }


      protected void Start()
      {
       //     bgmController.ChangeTutoBGM();
      }

      public virtual void NextScene()
      {

      }
}
