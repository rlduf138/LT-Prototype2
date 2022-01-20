using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageNewController : MonoBehaviour
{
      public Player player;
      public Transform activeCheckPoint;
      public GameObject conversationSystem;
      public GameObject imotion;
      void Start()
      {
            FadeController.Instance.TitleFadeOut();
      }

      void Update()
      {

      }

      public void CharacterReset()
      {
            Debug.Log("CharacterDamaged");
            player.DisableControl();
            player.m_body2d.velocity = Vector2.zero;
            FadeController.Instance.FadeIn(ResetPosition);
            //ResetPosition();
      }
      private void ResetPosition()
      {
            Debug.Log("StageController.ResetPosition");
            CharacterStartPosition();
            player.m_body2d.velocity = Vector2.zero;
            
            player.ResetCharacter();
            FadeController.Instance.OpenScene(player.ActiveControl);
      }
      public void CharacterStartPosition()
      {
            player.transform.parent = null;
            player.transform.position = activeCheckPoint.position;
      }

      public void StoryStart()
      {
            StartCoroutine(StoryCoroutine());
      }
      public IEnumerator StoryCoroutine()
      {
            player.m_canControl = false;

            // 느낌표
            imotion.SetActive(true);
            yield return new WaitForSeconds(3f);

            imotion.SetActive(false);
            // 대화 진행
            conversationSystem.SetActive(true);
            conversationSystem.GetComponent<ConversationSystem>().ActiveConversation();

      }
}
