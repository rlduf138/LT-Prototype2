using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : EnemyBase
{
      public override IEnumerator ActivePatrol()
      {
            /*  Debug.Log("ActivePatrol()");
              while (!isFind)
              {

                    if (!isFind)
                    {
                          StartCoroutine(Patrol());
                    }
                    yield return new WaitForSeconds(moveTime + waitTime);
              }
              Debug.Log("ActivePatrol End");*/
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
                        m_anim.SetBool("Run", true);
                        //transform.Translate(direction * speed * Time.deltaTime, Space.World);

                        // 공중에 뛰는거 안하게.
                        Vector3 dest = new Vector3(characterBase.transform.position.x, transform.position.y);

                        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);

                  }
            }
      }

      public override IEnumerator ChaseStop()
      {
            transform.position = Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
            StopCoroutine("Chase");
            isChasing = false;
            m_anim.SetBool("Run", false);
            yield return null;
      }

      public override IEnumerator Patrol()
      {
            /*Vector3 direction;
            if (!toRight)
            {
                  direction = patrolPos00.position - patrolPos01.position;
                  toRight = true;
            }
            else
            {
                  direction = patrolPos01.position - patrolPos00.position;
                  toRight = false;
            }
            direction.Normalize();

            if (direction.x < 0)
            {
                  Flip(false);
            }
            else
            {
                  Flip(true);
            }
            currentTime = 0f;
            m_anim.SetBool("Run", true);
            while (currentTime < moveTime)
            {

                  yield return new WaitForFixedUpdate();
                  if (!isFind)
                  {
                        transform.Translate(direction * speed * Time.deltaTime, Space.World);
                        currentTime += Time.deltaTime;
                  }
            }
            m_anim.SetBool("Run", false);
            //yield return new WaitForSeconds(patrolTime);*/
            yield return null;
      }
      
}
