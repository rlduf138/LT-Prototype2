using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeBlockController : MonoBehaviour
{
      public List<BrakeBlock> brakeBlocks;

      public void ResetAllBlock()
      {
            for (int i = 0; i < brakeBlocks.Count; i++)
            {
                  brakeBlocks[i].gameObject.SetActive(true);
                  brakeBlocks[i].ResetBlock();
            }
      }

      public void DisableAllBlock()
      {
            for (int i = 0; i < brakeBlocks.Count; i++)
            {
                  brakeBlocks[i].gameObject.SetActive(true);
            }
      }
}

