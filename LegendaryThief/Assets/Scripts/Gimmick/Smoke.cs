using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
      public BoxCollider2D coll;
      public float onTime;
      public float offTime = 0f;
      public float delayTime = 0f;
      public bool noAuto = false;
      public Animator m_animator;
      // Start is called before the first frame update
      void Start()
      {
            //StartCoroutine("Smoking");
      }
      private void OnEnable()
      {
            if(!noAuto)
                  StartCoroutine("Smoking");
      }
      // Update is called once per frame
      void Update()
      {

      }
      public IEnumerator Smoking()
      {
            yield return new WaitForSeconds(delayTime);

            while (true)
            {
                  SmokeOn();
                  yield return new WaitForSeconds(onTime);
                  SmokeOff();
                  yield return new WaitForSeconds(offTime);
            }
      }
      public void SmokeOff()
      {
            coll.enabled = false;
            m_animator.gameObject.SetActive(false);
            m_animator.SetBool("Active", false);
            
            // 끄는 애니메이션 적용.
      }
      public void SmokeOn()
      {
            coll.enabled = true;
            m_animator.gameObject.SetActive(true);
            m_animator.SetBool("Active", true);
            
      }
      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.gameObject.tag == "Player")
            {
                  Player characterBase = collision.gameObject.GetComponentInParent<Player>();
                  if (characterBase.isInvinsible == false)
                  {
                        characterBase.OnDamage(1, transform.position);  // 캐릭터 데미지 주고.
                        // 시작지점으로 보낸다.
                        //stage.CharacterDameged();
                  }
            }
      }
}
