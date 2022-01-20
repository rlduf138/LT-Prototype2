using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollider : MonoBehaviour
{
      public StageNewController stageNewController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void SaveCheckPoint()
      {
            stageNewController.activeCheckPoint = this.transform;
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Player ch = collision.GetComponent<Player>();
                  if(ch != null)
                  {
                        SaveCheckPoint();
                  }
            }
      }
}
