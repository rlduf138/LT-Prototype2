using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
      

      public float m_cameraMinX;
      public float m_cameraMaxX;
      public float m_cameraMinY;
      public float m_cameraMaxY;

      public int stageNumber;
      public List<Transform> respawnPosList;
      public Transform activeRespawnPos;

      public List<GameObject> colliders;
      //public List<FuelMachine> fuelMachines;
      public List<FuelMachine> fuelMachines;
      public bool[] fuelActives;
      public Deerg[] deergInfos;    // FuelMachine에 들어가있는 디어그
      public List<ActiveObject> activeObjects;
      public List<FallingTrap> fallingTraps;
      public List<GameObject> electroTraps;
      public List<Deerg> deergs;
      public bool[] deergResets;    // 디어그 초기화 여부

      public List<HiddenDoor> hiddenDoors;
      public List<BrakeBlockController> brakeBlockControllers;
      public List<MovingPlatform> movingPlatforms;
      
      void Start()
      {

      }

      void Update()
      {

      }

      public virtual void ActiveAllObject()
      {
            // Collider 포함 모든 오브젝트 작동.
            for(int i = 0; i<colliders.Count; i++)
            {
                  colliders[i].SetActive(true);
            }
      }
      public virtual void DisableAllObject()
      {
            for (int i = 0; i < colliders.Count; i++)
            {
                  colliders[i].SetActive(false);
            }
      }
      public virtual void SaveMapObject()
      {

      }
      public virtual void ResetStage()
      {
            // Deerg 먹고 오브젝트 작동시 Deerg 미생성
      }
}
