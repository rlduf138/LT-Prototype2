using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDecoController : MonoBehaviour
{

      public List<GameObject> objects;
      
      private void OnEnable()
      {
            StartCoroutine("ActiveBackDeco");
      }

      IEnumerator ActiveBackDeco()
      {
            while (true)
            {
                  float ran = Random.Range(6f, 9f);
                  yield return new WaitForSeconds(ran);

                  ActiveDeco();
            }
      }

      public void ActiveDeco()
      {
            int ran = Random.Range(0, objects.Count);
            
            Animator anim =  objects[ran].GetComponent<Animator>();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                  anim.SetTrigger("Active");
            }
          
      }

      public void Reset()
      {
            for(int i = 0; i<objects.Count; i++)
            {
                //  Animator anim =  objects[i].GetComponent<Animator>();
                //  if (anim.GetCurrentAnimatorStateInfo(0).IsName("Active") || anim.GetCurrentAnimatorStateInfo(0).IsName("End") )
                 // {
                        // 현재 애니메이션이 실행중이거나 끝났으면 초기화
                   //     Debug.Log("Reset");
                        objects[i].GetComponent<SpriteRenderer>().sprite = null;
                 // }
            }
      }
}
