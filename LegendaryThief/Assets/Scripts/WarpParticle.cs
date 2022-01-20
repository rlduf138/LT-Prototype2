using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpParticle : MonoBehaviour
{
      public GameObject firstParticle;
      public GameObject secParticle;

      // Start is called before the first frame update
      void Start()
      {
            StartCoroutine(Particle());
      }

     protected IEnumerator Particle()
      {
            firstParticle.SetActive(true);

            yield return new WaitForSeconds(1f);
            firstParticle.SetActive(false);
            secParticle.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            secParticle.SetActive(false);
      }
}
