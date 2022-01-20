using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Tutorial02 : Tutorial
{
      public CharacterBase characterBase;

      public GameObject talkPrefab;

      public Transform transform00;
      public Transform transform01;

      public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

      
      public GameObject lightGroup;

      public MissionObject warp;

      [Header("Conversation Order")]
      public int start_index00;
      public int count00;
      public int start_index01;
      public int count01;
    

      private new void Awake()
      {
            characterBase = FindObjectOfType<CharacterBase>();
            base.Awake();
            FadeController.Instance.OpenScene( Active);
      }

      new void Start()
      {
            characterBase.isTuto = true;
            
            //StartCoroutine(Move(transform00, Action00));
            //StartCoroutine(Move(transform00,  next += Action00));
            base.Start();

            warp.SetWarp(NextScene);
      }
      protected new void Active()
      {
            base.Active();
            StartCoroutine(Move(transform00, Action00));
      }
     
    
      public IEnumerator Move(Transform endTransform, Action action)
      {
            //StartCoroutine(Move(transform01, next += Action01));
            var t = 0f;
            var start = characterBase.transform.position;
            var end = endTransform.position;

            characterBase.m_Anim.SetBool("Run", true);

            while (t < 1f)
            {
                  t += Time.deltaTime / 2f;
                  characterBase.transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  yield return null;
            }
            characterBase.m_Anim.SetBool("Run", false);
            yield return new WaitForSeconds(0.5f);
            action();
            
      }
      public void Action00()
      {
            Debug.Log("Action00");
            ActiveTalk();
      }

      public void ActiveTalk()
      {
           // next = null;
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index00, count00,  Action01);
            
      }
      public void Action01()
      {
            StartCoroutine(LightOnOff());
      }

      public IEnumerator LightOnOff()
      {
            lightGroup.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            lightGroup.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            ActiveTalk2();
      }

      public void ActiveTalk2()
      {
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index01, count01, Action02);
            
      }
      public void Action02()
      {
            StartCoroutine(Move(transform01, Action03));
            
      }
      public void Action03()
      {
            WarpEffect();
      }
      public void WarpEffect()
      {
            StartCoroutine(warp.ActiveShader(NextScene));
      }

      public override void NextScene()
      {
            PlayerPrefs.SetInt("Tutorial", 1);
            FadeController.Instance.FadeIn( ActiveNextScene);
      }
      private void ActiveNextScene()
      {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Agit");
      }
}
