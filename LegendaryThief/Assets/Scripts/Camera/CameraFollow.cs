using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
      private Transform target;
      public float xMax, xMin, yMax, yMin;
      public Vector3 minTile, maxTile;
      public float lerpSpeed = 5f;
      
      public List<BackGround> backgrounds;

      [SerializeField]
      Tilemap tilemap;

      private CharacterBase characterBase;

      void Start()
      {
            target = GameObject.FindGameObjectWithTag("Player").transform;

            characterBase = target.GetComponent<CharacterBase>();

            float minClamp = Mathf.Clamp(target.position.x, xMin, xMax);
            float maxClamp = Mathf.Clamp(target.position.y, yMin, yMax);
            transform.position = new Vector3(minClamp, maxClamp, -10);
            // 타일 좌표가 가장 낮은것과 가장 높은것의 Vector3 값을 찾느다.
          //  minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
           // maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

          //  SetLimits(minTile, maxTile);

          
      }

      

      void LateUpdate()
      {
            float minClamp = Mathf.Clamp(target.position.x, xMin, xMax);
            float maxClamp = Mathf.Clamp(target.position.y, yMin, yMax);
            Vector3 desiredPosition = new Vector3(minClamp, maxClamp, -10);

          //  Debug.Log("DesiredPosition " + desiredPosition);

            Vector3 nextPos = Vector2.MoveTowards(transform.position, target.position, lerpSpeed * Time.deltaTime);
            transform.position = nextPos;
            transform.position = new Vector3(minClamp, maxClamp, -10);

            // 카메라 팔로우
            //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * lerpSpeed);
           for(int i = 0; i< backgrounds.Count; i++)
            {
                  backgrounds[i].MoveBackGround(desiredPosition);
            }
            
            //background.MoveBackGround(desiredPosition);
            // 카메라 이동시, 배경도 이동.
      }

      public void SetCameraPlayer()
      {
            target = FindObjectOfType<Player>().transform;
      }
      public void SetCameraHologram()
      {
            target = FindObjectOfType<Hologram>().transform;
      }

      // 카메라의 이동범위 결정.
      private void SetLimits(Vector3 minTile, Vector3 maxTile)
      {
            Camera cam = Camera.main;

            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            // 자동설정
          /*  xMin = minTile.x + width / 2;
            xMax = maxTile.x - width / 2;

            yMin = minTile.y + height / 2;
            yMax = maxTile.y - height / 2;*/
      }
}
