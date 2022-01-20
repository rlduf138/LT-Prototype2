using PixelSilo.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrder : Singleton<StageOrder>
{
      public List<string> stageOrder;
      public int currendOrder;
      void Start()
      {
            DontDestroyOnLoad(transform.gameObject);
      }



      void Update()
      {

      }
}
