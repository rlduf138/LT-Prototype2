using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deerg : MonoBehaviour
{
      public bool isReset = true;
      public bool isActive = false;
      public Animator m_animator;
      // Start is called before the first frame update
      void Start()
      {

      }
      private void OnEnable()
      {
            isActive = false;
      }
      // Update is called once per frame
      void Update()
      {

      }

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (collision.CompareTag("Player"))
            {
                  if (!isActive)
                  {
                        isActive = true;
                        Player player = collision.GetComponent<Player>();
                        player.deergs.Add(this);
                        //gameObject.SetActive(false);
                        StartCoroutine(GetDeerg());
                  }
            }
      }

      public IEnumerator GetDeerg()
      {
            m_animator.SetTrigger("Get");
            yield return new WaitForSeconds(0.8f);
            gameObject.SetActive(false);
      }
}
