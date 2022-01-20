using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
      public BoxCollider2D coll;
      public float onTime;
      public float offTime = 0f;
      public float delayTime = 0f;
      public bool noAuto = false;         // false : 자동으로 꺼졌다 켜졌다.  true : 계속 켜짐
      public Animator m_animator;
      // Start is called before the first frame update
      void Start()
      {
            //StartCoroutine("Smoking");
      }
      private void OnEnable()
      {
            if (!noAuto)
                  StartCoroutine("Smoking");
            else
                  SmokeOn();
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
                  Player ch = collision.GetComponent<Player>();
                  Hologram hologram = collision.gameObject.GetComponent<Hologram>();

                  if (ch != null)
                  {
                      
                              ch.OnDamage(1f, transform.position);
                              Debug.Log("SmokeHit");
                      
                  }
                  else if (hologram != null)
                  {
                       
                          //    hologram.OnDamage(1f, transform.position);
                              Debug.Log("SmokeHit");
                      
                  }
                  
            }
      }
}
