using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LivingEntity : MonoBehaviour
{
      public GameObject darkball_effect;

      public float startingHealth = 100f;
      public float health;
      public bool dead { get; protected set; }
      public bool hit { get; protected set; }
      public bool attack { get; protected set; }
      public event Action onDeath;

      public Transform spriteTrans;
      public Slider hpBar;
      public Image hpBarFill;

      public bool blind = false;

      public Text stateText;

      public Animator animator;

      public SpriteRenderer ch_sprite;

      // 생명 활성화시 상태 리셋
      protected virtual void OnEnable()
      {
            // 사망하지 않은 상태
            dead = false;
            // 체력을 시작 체력으로
            health = startingHealth;
            hpBar.maxValue = startingHealth;
            HpBarRefresh(health);
            // ch_sprite = spriteTrans.GetComponent<SpriteRenderer>();
      }

      // 데미지를 입는 기능
      public virtual void OnDamage(float damage, Vector2 hitPoint)
      {
            // 데미지만큼 체력 감소
            health -= damage;
            HpBarRefresh(health);
            //체력이 0 이하 && 아직 죽지않았다면 사망처리
            if (health <= 0 && !dead)
            {
                  Die();
            }
            hit = true; // 맞을때 멈추게 하거나 하는 용도
            StartCoroutine(HitEffect());
      }
      public IEnumerator HitEffect()
      {
            ch_sprite.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            ch_sprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            ch_sprite.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            ch_sprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            ch_sprite.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            ch_sprite.color = new Color(1f, 1f, 1f, 1f);


            yield return null;
      }
      // 체력 회복 기능
      public virtual void RestoreHealth(float newHealth)
      {
            if (dead)
            {
                  // 이미 죽었으면 회복 안함
                  return;

            }
            Debug.Log("RestoreHealth : +" + newHealth);
            // 체력 추가
            health += newHealth;
            HpBarRefresh(health);
      }

      // 사망 처리
      public virtual void Die()
      {
            Debug.Log("base.Die");
            // onDeath 이벤트에 등록된 메서드가 있으면 실행
            if (onDeath != null)
            {
                  onDeath();
            }
            // CharacterBase -> Dead 로 옮김
           /* if (animator != null)
            {
                  animator?.SetTrigger("dead");
            }
            // 사망상태를 참으로
            dead = true;
            tag = "Untagged";*/
      }

      public void Flip(float x)
      {
            Vector3 scale = spriteTrans.localScale;

            if (Mathf.Sign(x) != 0)
            {
                  scale.x = Mathf.Sign(x) * 1;

                  spriteTrans.localScale = scale;
            }
      }

      protected void HpBarRefresh(float health)
      {
            hpBar.value = health;
            float percent = hpBar.value / hpBar.maxValue;

            if (percent <= 0.3)
            {
                  // 색깔 빨간색
                  hpBarFill.color = Color.red;
            }
            else if (percent <= 0.7)
            {
                  // 색깔 노란색
                  hpBarFill.color = Color.yellow;
            }
            else if (percent <= 1)
            {
                  // 색깔 초록색
                  hpBarFill.color = Color.green;
            }
      }
}
