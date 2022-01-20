using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
      public float offsetX;
      public float offsetY;
      public float xSpeed = 5f;
      public float ySpeed = 2f;
      // Start is called before the first frame update
      void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }

      public void MoveBackGround(Vector3 vector3)
      {
            transform.position = new Vector3(vector3.x * xSpeed + offsetX, vector3.y * ySpeed +offsetY, vector3.z);
      }
}
