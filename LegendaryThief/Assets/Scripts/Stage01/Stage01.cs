using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
      public GameObject rainEffect;
      public Transform startPos;
      Player player;
      public GameObject startParticle;
     
      public CameraFollow cameraFolllow;
      
      public List<EnemyBase> enemyList;
      public DontDestroyInfo dontDestroyInfo;

      [Header("Audio")]
      private BGMController bgmController;
      public GameObject bgmPrefab;
      public GameObject menuPrefab;
      public AudioMixerSlider audiomixer;

      [Header("StageInfo")]
      public List<StageInfo> stageInfos;
      public StageInfo activeStageInfo;

      void Awake()
      {
            //Application.targetFrameRate = 60;

            Debug.Log("Stage0101 Awake");
            player = FindObjectOfType<Player>();
          
            bgmController = FindObjectOfType<BGMController>();
            if (bgmController == null)
            {
                  Debug.Log("BGMController Instantiate");
                  GameObject obj;
                  obj = Instantiate(bgmPrefab);
                  bgmController = obj.GetComponent<BGMController>();
            }
            audiomixer = FindObjectOfType<AudioMixerSlider>();
            if(audiomixer == null)
            {
                  GameObject obj;
                  obj = Instantiate(menuPrefab);
                  audiomixer = obj.GetComponent<AudioMixerSlider>();
            }

            dontDestroyInfo = FindObjectOfType<DontDestroyInfo>();
            dontDestroyInfo.stageInfo = stageInfos[dontDestroyInfo.stageInfoNumber];
            dontDestroyInfo.startPos = dontDestroyInfo.stageInfo.respawnPosList[dontDestroyInfo.startPosNumber];

            FadeController.Instance.OpenScene(Active);
      }

      private void Start()
      {
            
            Debug.Log("Stage0101 Start");
            bgmController = FindObjectOfType<BGMController>();
            bgmController.ChangeEphoBGM();
            // characterBase.OffSprite();
            //      characterBase.transform.position = startPos.position;

            InitCamera();
            InitStage();
            CharacterStartPosition();

            if(dontDestroyInfo.stageInfoNumber <= 2)
            {
                  RainOn();
            }
      }

      public void InitStage()
      {
            dontDestroyInfo.stageInfo.ActiveAllObject();
            if (dontDestroyInfo.stageInfoNumber != 0)
            {
                  player.prevStageinfo = stageInfos[dontDestroyInfo.stageInfoNumber - 1];
            }
      }
      public void InitCamera()
      {
            activeStageInfo = dontDestroyInfo.stageInfo;
            cameraFolllow.xMin = activeStageInfo.m_cameraMinX;
            cameraFolllow.xMax = activeStageInfo.m_cameraMaxX;
            cameraFolllow.yMin = activeStageInfo.m_cameraMinY;
            cameraFolllow.yMax = activeStageInfo.m_cameraMaxY;
        
      }

      public void CharacterStartPosition()
      {
            player.transform.parent = null;
            player.transform.position = dontDestroyInfo.startPos.position;
      }
      public void ChangeActiveStage()
      {
            activeStageInfo = dontDestroyInfo.stageInfo;
            InitCamera();
      }


      protected void Active()
      {
            //StartCoroutine(StartParticle());
      }
      protected IEnumerator StartParticle()
      {
         //   player.isTuto = true;

            GameObject particle = Instantiate(startParticle, player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);

           // player.isTuto = false;
        //    player.OnSprite();

            yield return new WaitForSeconds(0.3f);

            Destroy(particle);
      }

      public void CharacterDameged()
      {
            Debug.Log("CharacterDamaged");
            player.DisableControl();
            player.m_body2d.velocity = Vector2.zero;
            FadeController.Instance.FadeIn(ResetPosition);
            //ResetPosition();
      }
      private void ResetPosition()
      {
            Debug.Log("StageController.ResetPosition");
            CharacterStartPosition();
            player.m_body2d.velocity = Vector2.zero;
            activeStageInfo.ResetStage();
            player.ResetCharacter();
            FadeController.Instance.OpenScene(player.ActiveControl);
           // FadeController.Instance.FadeIn(ReLoadScene);
      }
      private void ResetPosition02()
      {
           
       //     characterBase.isTuto = false;
      }

      public void RainOn()
      {
            rainEffect.SetActive(true);
      }
      public void RainOff()
      {
            rainEffect.SetActive(false);
      }

      public void NextScene()
      {
            FadeController.Instance.FadeIn(ActiveNextScene);
      }
      private void ActiveNextScene()
      {
            //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial");
            UnityEngine.SceneManagement.SceneManager
                  .LoadSceneAsync(StageOrder.Instance.stageOrder[StageOrder.Instance.currendOrder++]);
      }
      private void ReLoadScene()
      {
            UnityEngine.SceneManagement.SceneManager
                 .LoadSceneAsync("Ehop01");
      }
}
