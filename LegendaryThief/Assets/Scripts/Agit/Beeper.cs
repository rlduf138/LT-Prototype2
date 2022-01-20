using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeper : Highlight
{
      CharacterBase characterBase;

      private new void Start()
      {
            base.Start();
            characterBase = FindObjectOfType<CharacterBase>();
      }
      // Start is called before the first frame update
      protected void Update()
      {
            if (Input.GetKeyUp(KeyCode.F) && isActive)
            {
                  characterBase.FlareShot();
                  
            }
      }
}
