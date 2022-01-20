using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : EnemyBase
{

      public float attackSpeed;
      public GameObject projectile;
      public Transform firePos;
      private float currentAttackTime;
      public bool canAttack;
      private void Update()
      {
            if (canAttack == false)
            {
                  if (currentAttackTime >= attackSpeed)
                  {
                        currentAttackTime = 0;
                        canAttack = true;
                  }else
                        currentAttackTime += Time.deltaTime;
            }
      }
      public override IEnumerator ActivePatrol()
      {
            yield return null;
      }

      public override IEnumerator Chase(CharacterBase characterBase)
      {
            if (!isChasing)
            {
                  isChasing = true;
                  Vector3 direction;
                  while (true)
                  {
                        direction = characterBase.transform.position - transform.position;
                        if (direction.x < 0)
                        {
                              Flip(false);
                        }
                        else
                        {
                              Flip(true);
                        }
                        yield return new WaitForFixedUpdate();
                        //  m_anim.SetBool("Run", true);

                        RotateFirePos(characterBase);
                        Vector3 dest = new Vector3(characterBase.transform.position.x, characterBase.transform.position.y);

                        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
                        if (canAttack)
                        {
                              Instantiate(projectile, firePos.position, firePos.localRotation);
                              canAttack = false;
                        }
                  }
            }
      }
      public void Attack()
      {
            var proj = Instantiate(projectile, firePos.position, firePos.localRotation);
            
      }
      public void RotateFirePos(CharacterBase characterBase)
      {
            Vector2 direction = (Vector2)characterBase.transform.position - (Vector2)firePos.position;
            firePos.up = direction;

            //Vector3 direction1 = characterBase.transform.position - firePos.position;
            //float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      }

      public override IEnumerator ChaseStop()
      {
            transform.position = Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
            StopCoroutine("Chase");
            isChasing = false;
           // m_anim.SetBool("Run", false);
            yield return null;
      }

      public override IEnumerator Patrol()
      {
            yield return null;
      }

    
}
