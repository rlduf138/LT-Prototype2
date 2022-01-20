using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoFuel : StageInfo
{
      public BackDecoController backDecoController;
      public override void ActiveAllObject()
      {
            base.ActiveAllObject();
            Debug.Log("ActiveAllObject");
            for (int i = 0; i < activeObjects.Count; i++)
            {
                  activeObjects[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < fuelMachines.Count; i++)
            {
                  fuelMachines[i].gameObject.SetActive(true);
                  if (fuelActives[i])
                  {
                        //fuelMachines[i].activeObject.Active();
                        fuelMachines[i].activeObject.InitActive();
                        fuelMachines[i].m_animator.SetBool("Active", true);
                        fuelMachines[i].isActive = true;
                        fuelMachines[i].deerg = deergInfos[i];
                  }
                  else if (!fuelActives[i])
                  {
                        //     fuelMachines[i].activeMark.SetActive(false);
                        fuelMachines[i].InitFuelMachine();
                  }
            }
            for (int i = 0; i < deergs.Count; i++)
            {
                  if (deergResets[i])
                  {
                        deergs[i].gameObject.SetActive(true);
                  }
            }
            for (int i = 0; i < fallingTraps.Count; i++)
            {
                  fallingTraps[i].ResetTrap();
                  fallingTraps[i].gameObject.SetActive(true);
            }
            for(int i = 0; i<electroTraps.Count; i++)
            {
                  electroTraps[i].SetActive(true);
            }
            for(int i = 0; i<hiddenDoors.Count; i++)
            {
                  hiddenDoors[i].gameObject.SetActive(true);
                  hiddenDoors[i].Init();
            }
            for(int i = 0; i<brakeBlockControllers.Count; i++)
            {
                  brakeBlockControllers[i].ResetAllBlock();
            }
            for(int i = 0; i<movingPlatforms.Count; i++)
            {
                  movingPlatforms[i].ResetPosition();
            }
            if (backDecoController != null)
            {
                  backDecoController.Reset();
                  backDecoController.gameObject.SetActive(true);
            }
      }
      public override void DisableAllObject()
      {
            base.DisableAllObject();
            Debug.Log("DisalbeAllObject");
            for (int i = 0; i < fuelMachines.Count; i++)
            {
                  // 연료주입구 비활성화
                  fuelMachines[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < activeObjects.Count; i++)
            {
                  activeObjects[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < deergs.Count; i++)
            {
                  deergs[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < fallingTraps.Count; i++)
            {
                  fallingTraps[i].ResetTrap();
            }
            for (int i = 0; i < electroTraps.Count; i++)
            {
                  electroTraps[i].SetActive(false);
            }
            for (int i = 0; i < brakeBlockControllers.Count; i++)
            {
                  brakeBlockControllers[i].DisableAllBlock();
            }
            for (int i = 0; i < movingPlatforms.Count; i++)
            {
                  movingPlatforms[i].ResetPosition();
            }
            if (backDecoController != null)
            {
                  backDecoController.Reset();
                  backDecoController.gameObject.SetActive(false);
            }
      }
      public override void SaveMapObject()
      {
            base.SaveMapObject();
            Debug.Log("SaveMapObject");
            // 현재 맵 상태 저장
       
            for (int i = 0; i < fuelActives.Length; i++)
            {
                  if (fuelMachines[i].isActive)
                  {
                        fuelActives[i] = true;
                        deergInfos[i] = fuelMachines[i].deerg;
                        fuelMachines[i].deerg.isReset = false;
                        
                  }
                  else
                  {
                        fuelActives[i] = false;
                  }
            }
            for (int i = 0; i < deergResets.Length; i++)
            {
                  if (deergs[i].isReset)
                        deergResets[i] = true;
                  else
                        deergResets[i] = false;
            }
            for (int i = 0; i < hiddenDoors.Count; i++)
            {
                  hiddenDoors[i].gameObject.SetActive(false);
                  hiddenDoors[i].Init();
            }
      }
      public override void ResetStage()
      {
            base.ResetStage();
            Debug.Log("ResetStage");
            ActiveAllObject();
          /*  for (int i = 0; i < deergs.Count; i++)
            {
                     if (deergResets[i])
                  {
                        // 연료 안넣어서 재생성 가능.
                        deergs[i].gameObject.SetActive(true);

                  }
            }
            for (int i = 0; i < fuelMachines.Count; i++)
            {
                  if (!fuelMachines[i].isPass)
                  {
                        //        fuelMachines[i].coll.enabled = true;
                  }
            }
            for (int i = 0; i < activeObjects.Count; i++)
            {

                  if (!activeObjects[i].isPass)
                  {
                        activeObjects[i].DeActive();
                  }
            }
            for (int i = 0; i < fallingTraps.Count; i++)
            {
                  fallingTraps[i].ResetTrap();
            }*/
      }
}
