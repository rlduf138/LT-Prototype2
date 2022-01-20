using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTrapController : ActiveObject
{
      public List<ElectroTrap> traps;

      public override void Active()
      {
            for(int i = 0; i<traps.Count; i++)
            {
                  traps[i].StopElectroTrap();
            }
            isActive = true;
      }
      public override void DeActive()
      {
            base.DeActive();
      }

}
