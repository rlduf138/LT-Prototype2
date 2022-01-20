using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCanvas : MonoBehaviour
{
      public Player player;

      void Start()
      {

      }

      void Update()
      {

      }

      public void OnDieClick()
      {
            player.OnDamage(1, transform.position);
      }
      public void OnReturnClick()
      {
            UnityEngine.SceneManagement.SceneManager
                     .LoadSceneAsync("CityEpho");
      }
}
