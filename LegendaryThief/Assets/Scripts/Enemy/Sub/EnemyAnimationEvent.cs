using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
      private PoliceBall policeBall;

      private void Start()
      {
            policeBall = GetComponentInParent<PoliceBall>();
      }


      public void ReadyToChase()
      {
            Debug.Log("Event ReadyToChase");
            policeBall.ChaseStart();
      }
      public void EndToPatrol()
      {
            Debug.Log("Event EndToPatrol");
            policeBall.StartPatrol();
      }
      public void ChaseToAttack()
      {
            Debug.Log("Event ChaseToAttack");
            policeBall.AttackAnimation();
      }
}
