using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : Highlight
{
      public int floor;
      public GameObject floor1;
      public GameObject floor2;

      protected void Update()
      {
            if (Input.GetKeyUp(KeyCode.F) && isActive)
            {
                  
                  if(floor == 1)
                  {
                        // 1층이면 2층으로 이동
                        character.transform.position = floor2.transform.position;
                  }else if(floor == 2)
                  {
                        // 2층이면 1층으로 이동
                        character.transform.position = floor1.transform.position;
                  }
            }
      }

}
