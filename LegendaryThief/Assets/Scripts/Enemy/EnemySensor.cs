using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
      public EnemyBase enemyBase;
      public CharacterBase characterBase;
   /*   public void OnTriggerStay2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  Debug.Log("Detect");
                  enemyBase.isFind = true;
                  enemyBase.m_anim.SetBool("Run", false);
                  collision.GetComponent<CharacterBase>().OnDamage(2f, collision.transform.position);
            }
      }*/

      public void OnTriggerEnter2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  Debug.Log("Enemy Detect");
                  characterBase = collision.GetComponent<CharacterBase>();
                  enemyBase.StartCoroutine("Chase", characterBase);
                  StopCoroutine("StopChasing");
                  
            }
      }
      private void OnTriggerStay2D(Collider2D collision)
      {
            if (characterBase != null)
            {
                  if (characterBase.gameObject.tag == "Untagged")
                  {
                        enemyBase.StartCoroutine("ChaseStop");
                  }else if(characterBase.gameObject.tag == "Player")
                  {
                        
                        enemyBase.StartCoroutine("Chase", characterBase);
                  }
            }
      }
      private void OnTriggerExit2D(Collider2D collision)
      {
            if (collision.GetComponent<CharacterBase>() == characterBase)
            {
                  Debug.Log("Enemy Detect Out");
                  //enemyBase.StartCoroutine("ChaseStop");
                  //characterBase = null;
                  StartCoroutine("StopChasing");
            }
      }

      public IEnumerator StopChasing()
      {
            yield return new WaitForSeconds(3f);
            enemyBase.StartCoroutine("ChaseStop");
            characterBase = null;
      }
}
