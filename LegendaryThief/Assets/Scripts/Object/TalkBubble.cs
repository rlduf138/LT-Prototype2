using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkBubble : MonoBehaviour
{

      public Sprite[] sprites;
      public Image image;
      public float anim_speed;
      public GameObject talkPanel;
      public TextMeshProUGUI text;
      public float talk_speed;
      private bool _isTalk = false;
      private bool _isAnim = false;
      private int talk_progress;    // 대화 진행도, 몇번째 대화 진행중인지.
      int n;

      [Header("Script Info")]
      public string[] text_list;

      // Start is called before the first frame update
      void Start()
      {

      }
      private void OnEnable()
      {
            
            StartCoroutine(BubbleAnimation());
      }
      public IEnumerator BubbleAnimation()
      {
            ResetBubble();
            _isAnim = true;
            int i = 0;
            image.sprite = sprites[0];
            yield return new WaitForSeconds(anim_speed);
            image.sprite = sprites[1];
            yield return new WaitForSeconds(anim_speed);
            i = 2;
            _isAnim = false;
            StartCoroutine(Talking());
            while (true)
            {
                  //2번부터 끝까지 반복
                  image.sprite = sprites[i];
                  yield return new WaitForSeconds(anim_speed);
                  i++;
                  if (i >= sprites.Length)
                  {
                        i = 2;
                  }
            }
      }
      // Update is called once per frame
      void Update()
      {
            if (Input.GetKeyDown("f") || Input.GetKeyDown("c"))
            {
                  if (!_isAnim)
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
                                    //  endTalk?.Invoke();
                                    talkPanel.SetActive(false);
                                    //   pCamera.upscaleRT = true;     // PixelPerfect On
                                    //   bloom.active = true;                     // Bloom On
                                    //   Destroy(this.gameObject);
                                    ResetBubble();
                              }
                        }
                  }
            }
      }
      public void ResetBubble()
      {
            n = 0;
            talk_progress = 0;
            text.text = "";
      }


      public IEnumerator Talking()
      {
            n = 0;


            while (n <= text_list[talk_progress].Length)
            {
                 // text.text = text_list[talk_progress].Replace("\\n", "\n");
                  _isTalk = true;

                  string str = "";
                  for (int i = 0; i < n; i++)
                  {
                        str += text_list[talk_progress][i];
                  }

                  text.text = str.Replace("\\n","\n");
                  
                  n++;

                  string space = " ";
                  string dash = "\\";
                 
                  if (n >= text_list[talk_progress].Length) { }
                  else if (text_list[talk_progress][n] == space[0]  )
                  {
                        // 띄어쓰기 타자기효과 제외
                        //   Debug.Log("띄어쓰기 타자기 제외.");
                        n++;
                        //break;
                  }else if(text_list[talk_progress][n] == dash[0])
                  {
                        n++;
                        Debug.Log("\\\\");
                  }
                  yield return new WaitForSeconds(talk_speed);
            }
            _isTalk = false;
            talk_progress++;
      }
}
