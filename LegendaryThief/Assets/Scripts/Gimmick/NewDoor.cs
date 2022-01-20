using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoor : ActiveObject
{
      public Animator _anim;
      public BoxCollider2D coll;

      public override void Active()
      {
            if (!isActive)
            {
                  _anim.SetTrigger("Open");
                  isActive = true;
            }
      }
      public override void DeActive()
      {
            if (isActive)
            {
                  _anim.SetTrigger("Close");
                  isActive = false;
            }
      }

      public void Open()
      {
            coll.enabled = false;
      }
      public void Close()
      {
            coll.enabled = true;
      }
}
