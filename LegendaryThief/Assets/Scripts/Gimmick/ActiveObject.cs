using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveObject : MonoBehaviour
{
      public bool isActive;
      public bool isPass = false;
      public virtual void Active()
      {
            
      }

     public virtual void DeActive()
      {

      }
      public virtual void InitActive()
      {
            Debug.Log("InitActiveObject - ActiveObject");
            Active();
      }
      public virtual void ResetActiveObject()
      {
            Debug.Log("ResetActiveObject - ActiveObject");
            DeActive();
      }
}
