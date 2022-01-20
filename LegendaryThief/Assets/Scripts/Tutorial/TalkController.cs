using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TalkController : MonoBehaviour
{
      public Image portrait_img;
      public TextMeshProUGUI talk_text;
      public TextMeshProUGUI name_text;
      public float talk_speed;
      public event Action endTalk;
      public GameObject talkPanel;

      private int talk_progress;    // 대화 진행도, 몇번째 대화 진행중인지.
      private int n;
      private bool _isTalk = false;
      PixelPerfectCamera pCamera;
      Volume gVolume;
      Bloom bloom;

      [Header("Script Info")]
      public string[] text_list;
      public int[] portrait_number;
      public int start_index;             // tuto1 총 대사중 시작 대사 번호
      public int script_count;            // 대사 수

      // Start is called before the first frame update
      protected void Start()
      {
            gVolume = FindObjectOfType<Volume>();
            gVolume.profile.TryGet<Bloom>(out bloom);
            bloom.active = false;                           // bloom off

            pCamera = Camera.main.GetComponent<PixelPerfectCamera>();
            pCamera.upscaleRT = false;          // PixelPerfect off
            //talkPanel.SetActive(true);
            //  StartCoroutine(Talking());

            // endtalk += 함수명 ()제외.
      }

      // Update is called once per frame
      void Update()
      {
            if (Input.anyKeyDown)
            {
                  if (_isTalk)
                  {
                        n = text_list[talk_progress].Length;      // 글자 진행중이면 빠르게.
                  }
                  else if (!_isTalk)
                  {
                        if (talk_progress < text_list.Length)
                        {
                              StartCoroutine(Talking());    // 대화 남아있으면 대화 진행.
                        }
                        else
                        {
                              Debug.Log("EndTalk");
                              endTalk?.Invoke();
                              talkPanel.SetActive(false);
                              pCamera.upscaleRT = true;     // PixelPerfect On
                              bloom.active = true;                     // Bloom On
                              Destroy(this.gameObject);
                        }
                  }
            }
      }

      public void Setting(int start, int count, Action action)
      {
            start_index = start;
            script_count = count;
            endTalk = action;

            text_list = new string[count];
            portrait_number = new int[count];
            // Script 에서 해당 대사내용 가져옴
            for (int i = 0; i < count; i++)
            {
                  text_list[i] = Script.Instance.text_list[start_index + i];
                  portrait_number[i] = Script.Instance.portrait_number[start_index + i];
            }

            talkPanel.SetActive(true);
            StartCoroutine(Talking());
      }

      public IEnumerator Talking()
      {
            n = 0;
            //portrait_img.sprite = portrait_list[ portrait_number[talk_progress] ];
            if (portrait_number[talk_progress] == 0)
            {
                  portrait_img.enabled = false;
            }
            else
            {
                  portrait_img.enabled = true;
                  
                  portrait_img.sprite = Script.Instance.ilust_list[portrait_number[talk_progress]];
                  portrait_img.SetNativeSize();
            }

            name_text.text = Script.Instance.name_list[portrait_number[talk_progress]];
            while (n <= text_list[talk_progress].Length)
            {
                  _isTalk = true;



                  string str = "";
                  for (int i = 0; i < n; i++)
                  {
                        str += text_list[talk_progress][i];
                  }

                  talk_text.text = str;

                  n++;

                  string space = " ";
                  if (n >= text_list[talk_progress].Length) { }
                  else if (text_list[talk_progress][n] == space[0])
                  {
                        // 띄어쓰기 타자기효과 제외
                        //   Debug.Log("띄어쓰기 타자기 제외.");
                        n++;
                        //break;
                  }
                  yield return new WaitForSeconds(talk_speed);
            }
            _isTalk = false;
            talk_progress++;
      }

      protected virtual void Active00()
      {

      }
      protected virtual void Active01()
      {

      }
      protected virtual void Active02()
      {

      }
}
