using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSystem : ActiveObject
{
      public List<Smoke> smokes;

      
      
      public override void Active()
      {
            for(int i = 0; i< smokes.Count; i++)
            {
                  smokes[i].SmokeOff();
            }
            isActive = true;
      }
      public override void DeActive()
      {
            for(int i = 0; i<smokes.Count; i++)
            {
                  smokes[i].SmokeOn();
            }
            isActive = false;
      }
      

}
