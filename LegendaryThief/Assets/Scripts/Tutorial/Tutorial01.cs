using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Tutorial01 : Tutorial
{
      public CharacterBase characterBase;

      public GameObject talkPrefab;
      public GameObject yellowArrow;
      public GameObject hiddenWall;
      public Transform transform00;
      public Transform transform01;

      public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

     
      BoxCollider2D boxcollider;

      [Header("Police")]
      public GameObject policePrefab;
      public GameObject[] policeList;
      public Transform[] policePosition;
     
      public GameObject dronePrefab;
      public Transform dronePosition;
      public Transform droneTargetPosition;
      public GameObject[] droneList;

     

      [Header("Conversation Order")]
      public int start_index00;
      public int count00;
      public int start_index01;
      public int count01;
      public int start_index02;
      public int count02;

      public GameObject hideTuto;
      public bool isMarkerTuto;
      public bool isWarpTuto;
      public bool isCheckHide;
      public bool isWarpSuccess;

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
            characterBase.onDeath += Death;
            //StartCoroutine(Move(transform00, Action00));
            //  StartCoroutine(ActiveTalk());
      }

      protected new void Active()
      {
            base.Active();
            StartCoroutine(Move(transform00, Action00));
      }

      // Update is called once per frame
      void Update()
      {
            if (isMarkerTuto && Input.GetKeyDown(KeyCode.X))
            {
                  hideTuto.SetActive(false);
                  isCheckHide = true;
                 // characterBase.Hide();
                  isMarkerTuto = false;
            }
            if(isWarpTuto&& !isWarpSuccess && Input.GetKeyDown(KeyCode.X))
            {
                  isWarpSuccess = true;
            }
      }

      public void Death()
      {
          /*  Debug.Log("Tutorial01 Death");

            talkObject05 = Instantiate(talkPrefab05, Vector3.zero, Quaternion.identity);
            TalkTuto0203 talk05 = talkObject05.GetComponent<TalkTuto0203>();
            talk05.tuto = this;
            talk05.portrait_img = portrait_img;
            talk05.talk_text = talk_text;
            talk05.talkPanel = talkPanel;

            talkObject05.SetActive(true);*/
      }

     
      

      public IEnumerator Move(Transform endTransform, Action action)
      {
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
            StartCoroutine(ActiveTurn());
      }
      public IEnumerator ActiveTurn()
      {
            yield return new WaitForSeconds(0.5f);
            characterBase.Flip(false);
            yield return new WaitForSeconds(0.5f);
            characterBase.Flip(true);
            yield return new WaitForSeconds(0.5f);
            Action01();
      }

      public void Action01()
      {
            ActiveTalk();
      }

      public void ActiveTalk()
      {
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index00, count00, Action02);
            
      }

      public void Action02()
      {
            characterBase.isTuto = false;
            hideTuto.SetActive(true);
            isMarkerTuto = true;
            StartCoroutine(CheckHide());
            
      }
    
      public IEnumerator CheckHide()
      {
            
            while (!isCheckHide)    // x키 눌렀는지 확인
            {
                  yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(PoliceActive());
      }

      public IEnumerator PoliceActive()
      {
            characterBase.isTuto = true;
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < policeList.Length; i++)
            {
                  yield return new WaitForSeconds(0.1f);
                  policeList[i].SetActive(true);
                  policeList[i].GetComponent<PoliceTuto>().MoveSecond();
            }
            //StartCoroutine(WaitForNextAction(9f, ActiveTalk01));
            StartCoroutine(Move(transform01, ActionWarp));
      }
      public void ActionWarp()
      {
            StartCoroutine(CheckWarpSuccess());

      }
      public IEnumerator CheckWarpSuccess()
      {
            hideTuto.SetActive(true);
            isWarpTuto = true;
            characterBase.isTuto = false;
            while (!isWarpSuccess)
            {
                  yield return new WaitForSeconds(0.1f);
            }
            characterBase.isTuto = true;
            hideTuto.SetActive(false);
            for (int i = 0; i < policeList.Length; i++)
            {
                  policeList[i].GetComponent<PoliceTuto>().MoveThird();
            }
            StartCoroutine(WaitForNextAction(3f, ActiveTalk01));
      }
      
      public IEnumerator WaitForNextAction(float second, Action action)
      {
           
            yield return new WaitForSeconds(second);

            action();
      }
      public void ActiveTalk01()
      {
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index01, count01, Action03);
            
      }

      public void Action03()
      {
            characterBase.Flip(false);
            StartCoroutine(DroneActive());
      }
      public IEnumerator DroneActive()
      {
            for (int i = 0; i < droneList.Length; i++)
            {
                  Destroy(droneList[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                  GameObject drone = Instantiate(dronePrefab, dronePosition.position, Quaternion.identity);
                  droneList[i] = drone;
                  DroneTuto enemy = drone.GetComponent<DroneTuto>();
                  enemy.patrolPos00 = droneTargetPosition;
                  enemy.moveTime = 1 + i * 0.3f;
                  yield return new WaitForSeconds(0.3f);
            }
            StartCoroutine(WaitForNextAction(3f, ActiveTalk02));
            yield return null;
      }
      public void ActiveTalk02()
      {
            GameObject talkObject = Instantiate(talkPrefab, Vector3.zero, Quaternion.identity);
            TalkController talkController = talkObject.GetComponent<TalkController>();
            talkController.portrait_img = portrait_img;
            talkController.talk_text = talk_text;
            talkController.name_text = name_text;
            talkController.talkPanel = talkPanel;
            talkController.Setting(start_index02, count02, Action04);
            
      }
      public void Action04()
      {
            hiddenWall.SetActive(true);
            characterBase.isTuto = false;
            characterBase.canMove = true;
            yellowArrow.SetActive(true);
      }

      public void Retry()
      {
           /* for (int i = 0; i < policeList.Length; i++)
            {
                  // 기존 경찰 삭제
                  Destroy(policeList[i]);
            }

            characterBase.health = 1f;
            characterBase.dead = false;
            characterBase.transform.position = transform00.position;

            for (int i = 0; i < 2; i++)
            {
                  GameObject police = Instantiate(policePrefab, policePosition[i].position, Quaternion.identity);
                  policeList[i] = police;
                  EnemyBase enemy = police.GetComponent<EnemyBase>();
                  enemy.patrolPos00 = policeTransform00;
                  enemy.patrolPos01 = policeTransform01;
            }
            for (int i = 2; i < 4; i++)
            {
                  GameObject police = Instantiate(policePrefab, policePosition[i].position, Quaternion.identity);
                  policeList[i] = police;
                  EnemyBase enemy = police.GetComponent<EnemyBase>();
                  enemy.patrolPos00 = policeTransform01;
                  enemy.patrolPos01 = policeTransform00;
            }
            StartCoroutine(DroneActive());*/
      }


      public override void NextScene()
      {
            FadeController.Instance.FadeIn( ActiveNextScene);
      }
      private void ActiveNextScene()
      {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial03");
      }
}
