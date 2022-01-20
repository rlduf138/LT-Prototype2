using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTrap : MonoBehaviour
{
      public BoxCollider2D boxCollider2D;
      public Animator m_animator;

      void Start()
      {

      }
      private void OnEnable()
      {
            StartCoroutine(ActiveAnimator());
      }
      void Update()
      {

      }

      public IEnumerator ActiveAnimator()
      {
            float ran = Random.Range(0f, 0.8f);
            yield return new WaitForSeconds(ran);
            m_animator.SetTrigger("Active");
      }

      public void StopElectroTrap()
      {
            m_animator.SetTrigger("Idle");
            boxCollider2D.enabled = false;
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  Player characterBase = collision.GetComponentInParent<Player>();
                  Hologram hologram = collision.gameObject.GetComponent<Hologram>();

                  if (characterBase != null)
                  {
                        characterBase.OnDamage(1, transform.position);  // 캐릭터 데미지 주고.
                                                                        // 시작지점으로 보낸다.
                                                                        //stage.CharacterDameged();
                  }
                  else if (hologram != null)
                  {
                        hologram.OnDamage(1, transform.position);
                  }
            }
      }

}
