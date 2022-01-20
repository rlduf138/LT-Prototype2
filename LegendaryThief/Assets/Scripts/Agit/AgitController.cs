using PixelSilo.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgitController : Singleton<AgitController>
{
      private BGMController bgmController;
      public GameObject bgmPrefab;
      private CharacterBase characterBase;
      public MissionObject warp;
      public GameObject stageOrderObject;

      public List<string> stageList;

      protected void Awake()
      {
            bgmController = FindObjectOfType<BGMController>();
            if (bgmController == null)
            {
                  GameObject obj;
                  obj = Instantiate(bgmPrefab);
                  bgmController = obj.GetComponent<BGMController>();
            }
            //PlayerPrefs.SetInt("Warp", 1);

            FadeController.Instance.OpenScene(Active);
      }

      protected void Active()
      {
            if (PlayerPrefs.GetInt("Warp") == 1)
            {
                  StartCoroutine(warp.ComeShader());
                  
            }
      }

      void Start()
      {
            //bgmController.ChangeAgitBGM();

            warp.SetWarp(NextScene);
            characterBase = FindObjectOfType<CharacterBase>();
            if (PlayerPrefs.GetInt("Warp") == 1)
            {
                  warp.GetComponent<SpriteRenderer>().enabled = false;
                  characterBase.OffSprite();
                  characterBase.transform.position = warp.gameObject.transform.position;
            }
      }

      void Update()
      {

      }

      public void NextScene()
      {
            CreateStageOrder();
            FadeController.Instance.FadeIn( ActiveNextScene);
      }
      private void ActiveNextScene()
      {
            //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial");
            UnityEngine.SceneManagement.SceneManager
                  .LoadSceneAsync(StageOrder.Instance.stageOrder[StageOrder.Instance.currendOrder++]);
      }

      public void CreateStageOrder()
      {
            GameObject stageobj = Instantiate(stageOrderObject, transform.position, Quaternion.identity);
            StageOrder stageOrder = stageobj.GetComponent<StageOrder>();

            // 중간보스, 정비, 최종보스는 뒷번호로 빼고.
            // 돌리기.
            for(int i = 0; i<6; i++)
            {
                  int ran = Random.Range(0, stageList.Count - 3); // 정비, 최종보스는 마지막 2개의 리스트
                  stageOrder.stageOrder.Add(stageList[ran]);
                  stageList.RemoveAt(ran);
            }
            stageOrder.stageOrder.Add(stageList[stageList.Count - 2]);  // 정비소 순서 배정.

            int ran2 = Random.Range(0, stageList.Count - 3); 
            stageOrder.stageOrder.Add(stageList[ran2]);
            stageList.RemoveAt(ran2);

            stageOrder.stageOrder.Add(stageList[stageList.Count-1]);    // 최종보스 순서 배정
            stageOrder.stageOrder.Add("Agit");
      }
}
