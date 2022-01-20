using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
      public EnemyBase enemyBase;

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if(collision.tag == "Player")
            {
                  enemyBase.Attack(collision.GetComponent<CharacterBase>());
            }
      }

}
