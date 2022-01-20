using PixelSilo.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyInfo : Singleton<DontDestroyInfo>
{
      DontDestroyInfo[] checker;
      public StageInfo stageInfo;
      public Transform startPos;
      public int stageInfoNumber;
      public int startPosNumber;
      
      private void Awake()
      {
            //  중복 체크 후 새로 생성되는거 제거
            checker = FindObjectsOfType<DontDestroyInfo>();
            if (checker.Length >= 2)
            {
                  Debug.Log("DontDestroyInfo Destroy");
                  Destroy(this.gameObject);
            }
            //DontDestroyOnLoad(transform.gameObject);
      }

      void Start()
      {

      }

      void Update()
      {

      }
}
