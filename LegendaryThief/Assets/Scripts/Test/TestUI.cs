using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestUI : MonoBehaviour
{
      Player player;

      public GameObject testPanel;
      public GameObject optionPanel;
      public MovingPlatform movingPlatform;

      public Text jumpForceText;
      public Text gravityText;
      public Text moveText;
      public Text slideText;
      public Text dashText;
      public Text movingSpeedText;
      public Text movingTimeText;

      public InputField jumpForceInput;
      public InputField gravityInput;
      public InputField moveInput;
      public InputField slideInput;
      public InputField dashInput;
      public InputField movingSpeedInput;
      public InputField movingTimeInput;

      public float jumpForce;
      public float gravityScale;
      public float moveSpeed;
      public float slideSpeed;
      public float dashPower;
      public float movingSpeed;
      public float movingTime;
      public AnimationClip anim;

      public InputField stageInput;
      public Stage01 stage;

      // Start is called before the first frame update
      void Start()
      {
            player = FindObjectOfType<Player>();

            GetPlayerData();
      }

      public void MoveStage()
      {
           
            

            stage.activeStageInfo = stage.stageInfos[int.Parse(stageInput.text)];
            stage.cameraFolllow.xMin = stage.activeStageInfo.m_cameraMinX;
            stage.cameraFolllow.xMax = stage.activeStageInfo.m_cameraMaxX;
            stage.cameraFolllow.yMin = stage.activeStageInfo.m_cameraMinY;
            stage.cameraFolllow.yMax = stage.activeStageInfo.m_cameraMaxY;

            stage.dontDestroyInfo.stageInfo = stage.activeStageInfo;
            stage.dontDestroyInfo.startPos = stage.activeStageInfo.respawnPosList[0];
            stage.dontDestroyInfo.stageInfoNumber = int.Parse(stageInput.text);
            stage.dontDestroyInfo.startPosNumber = 0;


            stage.CharacterDameged();
          //  stage.InitCamera();
            Player player = FindObjectOfType<Player>();
            player.transform.position = stage.activeStageInfo.respawnPosList[0].position;
      }

      public void TestPanelOn()
      {
            testPanel.SetActive(true);
            optionPanel.SetActive(false);
      }
      public void TestPanelOff()
      {
            testPanel.SetActive(false);
            optionPanel.SetActive(true);
      }

      public void GetPlayerData()
      {
            jumpForce = player.TestGetJumpForce();
            gravityScale = player.TestGetGravity();
            moveSpeed = player.TestGetMoveSpeed();
            slideSpeed = player.TestGetSlideSpeed();
            dashPower = player.TestGetDashPower();
            movingSpeed = movingPlatform.speed;
            movingTime = movingPlatform.time;

            jumpForceText.text = jumpForce.ToString();
            gravityText.text = gravityScale.ToString();
            moveText.text = moveSpeed.ToString();
            slideText.text = slideSpeed.ToString();
            dashText.text = dashPower.ToString();
            movingSpeedText.text = movingSpeed.ToString();
            movingTimeText.text = movingTime.ToString();
            
      }

      public void Set()
      {
            player.TestSetting(float.Parse(jumpForceInput.text), float.Parse(gravityInput.text)
                  , float.Parse(moveInput.text), float.Parse(slideInput.text), float.Parse(dashInput.text));
           
            GetPlayerData();
      }

      public void SetJF()
      {
            player.TestSetJumpForce(float.Parse(jumpForceInput.text));
            GetPlayerData();
      }
      public void SetGravity()
      {
            player.TestSetGravity(float.Parse(gravityInput.text));
            GetPlayerData();
      }
      public void SetMoveSpeed()
      {
            player.TestSetMoveSpeed(float.Parse(moveInput.text));
            GetPlayerData();
      }
      public void SetSlide()
      {
            player.TestSetSlideSpeed(float.Parse(slideInput.text));
            GetPlayerData();
      }
      public void SetDash()
      {
            player.TestSetDashPower(float.Parse(dashInput.text));
            GetPlayerData();
      }
      public void SetMFSpeed()
      {
            movingPlatform.speed = float.Parse(movingSpeedInput.text);
            GetPlayerData();
      }
      public void SetMFTime()
      {
            movingPlatform.time = float.Parse(movingTimeInput.text);
            GetPlayerData();
      }
}
