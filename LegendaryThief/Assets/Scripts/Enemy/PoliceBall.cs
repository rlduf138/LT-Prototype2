using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBall : MonoBehaviour
{
      public SpriteRenderer ch_sprite;
      public Animator m_anim;
      private Rigidbody2D rgdb2d;
      public CharacterBase cBase;

      [Header("Status")]
      public float patrolSpeed;
      public float chaseSpeed;
      public float attackSpeed;
      private float currentAttackTime;

      [Header("Range")]
      public Transform patrolLeft;
      public Transform patrolRight;
      public Transform chaseLeft;
      public Transform chaseRight;

      [Header("State")]
      public bool canAttack;
      public bool canMove;
      public bool isAttacking;
      public bool isCoChasing;
      public bool isCoPatrol;

      [Header("Collider")]
      public GameObject patrolCollider;
      public GameObject chaseCollider;
      public GameObject attackRangeCollider;
      public GameObject attackCollider;

      void Start()
      {
            rgdb2d = GetComponent<Rigidbody2D>();
            StartPatrol();
      }

      void Update()
      {

            if (currentAttackTime < attackSpeed)
            {
                  currentAttackTime += Time.deltaTime;
            }
            else
            {
                  canAttack = true;
            }
      }

      public void StopActive()
      {
            isCoChasing = false;
            isCoPatrol = false;
            StopAllCoroutines();
            patrolCollider.SetActive(false);
            chaseCollider.SetActive(false);
            attackRangeCollider.SetActive(false);
            attackCollider.SetActive(false);
            rgdb2d.gravityScale = 0f;
            m_anim.SetBool("Chase", false);
            //  m_anim.SetTrigger("End");
            //    m_anim.SetBool("Patrol", true);
            m_anim.Play("Patrol");
      }
      public void StartActive()
      {
            StartPatrol();
            patrolCollider.SetActive(true);
            
            rgdb2d.gravityScale = 1f;
      }
      protected void Flip(bool bLeft)
      {
            ch_sprite.transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
      }

      public void ReadyToChase(CharacterBase characterBase)
      {
            Debug.Log("PoliceBall ReadyToChase");
            cBase = characterBase;

            StopCoroutine("Patrol");
            isCoPatrol = false;

            // ChaseTrigger 키기
            patrolCollider.SetActive(false);
            chaseCollider.SetActive(true);

            m_anim.SetBool("Patrol", false);
            m_anim.SetTrigger("Ready");
            //m_anim.Play("Ready");

          //  ChaseStart();
      }
      public void ChaseStart()
      {
            // Animator Event
            Debug.Log("PoliceBall ChaseStart");
            attackRangeCollider.SetActive(true);
            if (isCoChasing)
            {
                  StopCoroutine("Chase");
            }
            if(cBase !=null)
                  StartCoroutine("Chase");

      }
      public IEnumerator Chase()
      {
            Vector3 direction;
            isCoChasing = true;
            m_anim.SetBool("Chase", true);
           // yield return new WaitForSeconds(0.3f);
            Debug.Log("PoliceBall Chase()");
            while (true)
            {
                  m_anim.SetBool("Chase", true);
                  //m_anim.Play("Chase");
                  yield return new WaitForFixedUpdate();

                  direction = cBase.transform.position - transform.position;
                  if (direction.x < 0) Flip(false);
                  else Flip(true);


                  Vector3 dest = new Vector3(cBase.transform.position.x, transform.position.y);

                  if (!isAttacking)       // 공격 중 이동 못하게
                  {
                        transform.position = Vector3.MoveTowards(transform.position, dest, chaseSpeed * Time.deltaTime);
                        if (transform.position.x <= chaseLeft.position.x)
                        {
                              transform.position = new Vector2(chaseLeft.position.x, transform.position.y);
                        }
                        else if (transform.position.x >= chaseRight.position.x)
                        {
                              transform.position = new Vector2(chaseRight.position.x, transform.position.y);
                        }
                  }
            }
      }

      public void EndChase()
      {
           Debug.Log("PoliceBall EndChase");
            if (isCoChasing)
            {
                  Debug.Log("PoliceBall EndChase true");
                  StopCoroutine("Chase");

                  isCoChasing = false;
                  m_anim.SetBool("Chase", false);
                  m_anim.SetTrigger("End");
                  //m_anim.Play("End");
                  patrolCollider.SetActive(true);
                  chaseCollider.SetActive(false);
                  attackRangeCollider.SetActive(false);
                  cBase = null;

                  
            }
             //StartCoroutine(EndChaseGap());
      }
      public IEnumerator EndChaseGap()
      {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("PoliceBall EndChase");
            if (isCoChasing)
            {
                  Debug.Log("PoliceBall EndChase true");
                  StopCoroutine("Chase");

                  isCoChasing = false;
                  m_anim.SetBool("Chase", false);
                  m_anim.SetTrigger("End");
                  
                  patrolCollider.SetActive(true);
                  chaseCollider.SetActive(false);
                  attackRangeCollider.SetActive(false);
                  cBase = null;


            }
      }

      public void AttackRangeStart()
      {
            // 공격범위 콜라이더 들어오면
            Debug.Log("AttackRangeStart");
            isAttacking = true;
            StartCoroutine("Attack");
      }

      public void AttackRangeEnd()
      {
            Debug.Log("AttackRangeEnd");
            isAttacking = false;
            StopCoroutine("Attack");
      }

      public IEnumerator Attack()
      {
            Debug.Log("PoliceBall Attack()");
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                  yield return new WaitForFixedUpdate();

                  if (canAttack)
                  {
                        // attack animation
                        m_anim.SetTrigger("Attack");
                        // m_anim.Play("Attack");
                        currentAttackTime = 0f;
                        canAttack = false;
                       // isAttacking = false;
                  }

            }
      }
      public void AttackAnimation()
      {
            StartCoroutine(AttackColliderOn());
      }
      public IEnumerator AttackColliderOn()
      {
            attackCollider.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            attackCollider.SetActive(false);
      }

      public void StartPatrol()
      {
            Debug.Log("PoliceBall StartPatrol");
            // Start, AnimationEvent
            if (isCoPatrol)
            {
                  StopCoroutine("Patrol");
            }
            StartCoroutine("Patrol");
      }

      public IEnumerator Patrol()
      {
            Debug.Log("PoliceBall Patrol()");
     
                 isCoPatrol = true;
            int where = Random.Range(0, 2);
            bool isLeft;
            if (where == 0)
                  isLeft = true;
            else
                  isLeft = false;

            StopCoroutine("Chase");
            m_anim.SetBool("Chase", false);
            patrolCollider.SetActive(true);
            chaseCollider.SetActive(false);
            attackRangeCollider.SetActive(false);

            Vector3 direction;
            while (true)
            {
                  if (isLeft)
                  {
                        m_anim.SetBool("Patrol", true);
                        //m_anim.Play("Patrol");
                        direction = patrolLeft.position - transform.position;

                        if (direction.x < 0) Flip(false);
                        else Flip(true);

                        yield return new WaitForFixedUpdate();


                        Vector3 dest = new Vector3(patrolLeft.position.x, transform.position.y);

                        transform.position = Vector3.MoveTowards(transform.position, dest, patrolSpeed * Time.deltaTime);
                        if (transform.position.x <= patrolLeft.position.x)
                        {
                              m_anim.SetTrigger("Idle");
                              m_anim.SetBool("Patrol", false);
                              //m_anim.Play("Idle");
                              float changeTime = Random.Range(0.5f, 1.5f);
                              yield return new WaitForSeconds(changeTime);
                              isLeft = false;
                        }
                  }
                  else if (!isLeft)
                  {
                        m_anim.SetBool("Patrol", true);
                        // m_anim.Play("Patrol");
                        direction = patrolRight.position - transform.position;

                        if (direction.x < 0) Flip(false);
                        else Flip(true);

                        yield return new WaitForFixedUpdate();


                        Vector3 dest = new Vector3(patrolRight.position.x, transform.position.y);

                        transform.position = Vector3.MoveTowards(transform.position, dest, patrolSpeed * Time.deltaTime);
                        if (transform.position.x >= patrolRight.position.x)
                        {
                              m_anim.SetTrigger("Idle");
                              m_anim.SetBool("Patrol", false);
                              //m_anim.Play("Idle");
                              float changeTime = Random.Range(0.5f, 1.5f);
                              yield return new WaitForSeconds(changeTime);
                              isLeft = true;
                        }
                  }
            }
      }

}
