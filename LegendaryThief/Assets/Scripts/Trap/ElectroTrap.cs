using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTrap : MonoBehaviour
{
  
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
}
