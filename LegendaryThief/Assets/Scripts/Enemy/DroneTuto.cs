using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTuto : EnemyBase
{
     
      

      public override IEnumerator ActivePatrol()
      {
            StartCoroutine(Patrol());
            yield return null;
      }

      
      public override IEnumerator Chase(CharacterBase characterBase)
      {
            throw new System.NotImplementedException();
      }

      public override IEnumerator ChaseStop()
      {
            throw new System.NotImplementedException();
      }

      public override IEnumerator Patrol()
      {
            Debug.Log("Drone Patrol");
            Vector3 direction;
            
            direction = patrolPos00.position - transform.position;

            direction.Normalize();


            while (currentTime < moveTime)
            {
                  yield return new WaitForFixedUpdate();
                  transform.Translate(direction * speed * Time.deltaTime, Space.World);
                  currentTime += Time.deltaTime;
            }
      }

}
