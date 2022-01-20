using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCollider : MonoBehaviour
{
      public CameraFollow cameraFollow;
      public Stage01 stage;
      public float camera_x;
      public float camera_y;
      Player player;
      public GameObject storyObject;
      // Start is called before the first frame update
      void Start()
      {
      }

      // Update is called once per frame
      void Update()
      {
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  player = collision.GetComponent<Player>();
                  player.m_canControl = false;
                  FadeController.Instance.FadeIn(GoToStory);
            }
      }

      public void GoToStory()
      {
            player.m_canControl = false;
            stage.cameraFolllow.xMin = camera_x;
            stage.cameraFolllow.xMax = camera_x;
            stage.cameraFolllow.yMin = camera_y;
            stage.cameraFolllow.yMax = camera_y;
            storyObject.SetActive(true);
            //FadeController.Instance.OpenScene();
           
      }
}
