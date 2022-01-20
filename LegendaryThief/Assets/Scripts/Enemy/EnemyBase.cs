using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
      private Stage01 stage;
      public SpriteRenderer ch_sprite;
      public float speed;
      public float waitTime;
      public float moveTime;
      private float currentPatrolTime;
      public Animator m_anim;

      public Transform patrolPos00;
      public Transform patrolPos01;
      public Transform startPos;

      public bool isFind;
      public bool toRight;
      public bool isChasing;
      protected float currentTime;

      // Start is called before the first frame update
      void Start()
      {
            StartCoroutine(ActivePatrol());
            stage = FindObjectOfType<Stage01>();
      }

      // Update is called once per frame
      void Update()
      {

      }
      public void Init()
      {
            isFind = false;
            currentTime = 0;
            StopCoroutine(Patrol());
            StartCoroutine(ActivePatrol());
      }

      public void Attack(CharacterBase characterBase)
      {
            characterBase.OnDamage(1, transform.position);
           // stage.CharacterDameged();
      }
      public void ResetPosition()
      {
            transform.position = startPos.position;
            StartCoroutine("ChaseStop");
            GetComponentInChildren<EnemySensor>().characterBase = null;
      }

      public abstract IEnumerator ActivePatrol();
      public abstract IEnumerator Patrol();
      public abstract IEnumerator Chase(CharacterBase characterBase);
      public abstract IEnumerator ChaseStop();
      protected void Flip(bool bLeft)
      {
            ch_sprite.transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
      }
}
