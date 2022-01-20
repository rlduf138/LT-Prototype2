using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryElevator : MonoBehaviour
{
      public Transform upTransform;
      public Transform playerTransform;
      public float speed;
      public StageNewController stageController;

      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }

      public IEnumerator MoveUp()
      {
            Debug.Log("MoveUp");
            yield return new WaitForSeconds(1f);

            
            bool checker = true;
            while (checker)
            {
                  transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

                  if (transform.position.y >= upTransform.position.y)
                  {
                        checker = false;
                  }

                  yield return new WaitForFixedUpdate();
            }

           transform.position = upTransform.position;
            stageController.StoryStart();
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Player player = collision.GetComponent<Player>();
                  player.m_canControl = false;
                  player.m_body2d.velocity = Vector3.zero;
                  player.transform.position = playerTransform.position;

                  collision.transform.parent = transform;
                  StartCoroutine("MoveUp");
                 
                 
            }
      }
}
