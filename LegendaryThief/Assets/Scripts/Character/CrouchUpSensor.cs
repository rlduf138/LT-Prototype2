using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchUpSensor : MonoBehaviour
{
      public bool isTouch = true; 

      
      
      public bool GetState()
      {
            return isTouch;
      }

     
      private void OnTriggerStay2D(Collider2D collision)
      {
            if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))
            {
                  isTouch = true;
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if(collision.CompareTag("Wall") || collision.CompareTag("Ground"))
            {
                  isTouch = false;
            }
      }
}
