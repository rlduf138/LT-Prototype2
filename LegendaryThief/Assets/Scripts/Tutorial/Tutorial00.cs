using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class Tutorial00 : Tutorial
{
      public CharacterBase characterBase;

      public GameObject talkPrefab;
      public GameObject jumpTuto;
      public GameObject moveTuto;
      public GameObject yellowArrow;
      public Transform transform00;

      public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

      BoxCollider2D boxcollider;


      [Header("Conversation Order")]
      public int start_index00;
      public int count00;
      public int start_index01;
      public int count01;

      // Start is called before the first frame update
      private new void Awake()
      {
            boxcollider = GetComponent<BoxCollider2D>();
            characterBase = FindObjectOfType<CharacterBase>();
            base.Awake();
            FadeController.Instance.OpenScene( Active);
      }
      new void Start()
      {
            base.Start();
            characterBase.isTuto = true;

           // StartCoroutine(WaitForStart());
            //ActiveTalk00();
         //   StartCoroutine(Move(transform00, next+=Action00));
            //  StartCoroutine(ActiveTalk());
      }

      protected new void Active()
      {
            base.Active();
            StartCoroutine(WaitForStart());
      }

      // Update is called once per frame
      void Update()
      {

      }
      public IEnumerator WaitForStart()
      {
            yield return new WaitForSeconds(0.5f);
            ActiveTalk00();
      }

      public IEnumerator Move(Transform endTransform, Action action)
      {
            var t = 0f;
            var start = characterBase.transform.position;
            var end = endTransform.position;

            characterBase.m_Anim.SetBool("Run", true);

            while (t < 1f)
            {
                  t += Time.deltaTime / 1f;
                  characterBase.transform.position = Vector3.LerpUnclamped(start, end, curve.Evaluate(t));
                  yield return null;
            }
            characterBase.m_Anim.SetBool("Run", false);

           

            yield return new WaitForSeconds(0.5f);
            action();
      }

      public void ActiveTalk00()
      {
            PixelPerfectCamera pCamera =  Camera.main.GetComponent<PixelPerfectCamera>();
            pCamera.upscaleRT = false;
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index00, count00, Action00);

      }

      public void Action00()
      {
            StartCoroutine(Move(transform00, Action01));
            //   StartCoroutine(ActiveTalk00());
            //StartCoroutine(ActiveTurn());
      }

      public void Action01()
      {
            StartCoroutine(ActiveTurn());
            Debug.Log("Action01");
      }
      public IEnumerator ActiveTurn()
      {
            yield return new WaitForSeconds(0.5f);
            characterBase.Flip(false);
            yield return new WaitForSeconds(0.5f);
            characterBase.Flip(true);
            yield return new WaitForSeconds(0.5f);
            Action02();
      }

      public void Action02()
      {
            StartCoroutine(ActiveTalk01());
      }
      public IEnumerator ActiveTalk01()
      {
            yield return new WaitForSeconds(1f);

            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index01, count01, Action03);

            //talkObject01.SetActive(true);
      }

      public void Action03()
      {
            characterBase.isTuto = false;
            moveTuto.SetActive(true);
      }

      public void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.tag == "Player")
            {
                  
                  moveTuto.SetActive(false);
                  jumpTuto.SetActive(true);
                  characterBase.canJump = true;
            }
      }
      public void OnTriggerExit2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  jumpTuto.SetActive(false);
                  yellowArrow.SetActive(true);
            }
      }

     
      public override void NextScene()
      {
            FadeController.Instance.FadeIn(ActiveNextScene);
      }
      private void ActiveNextScene()
      {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial02");
      }
}
