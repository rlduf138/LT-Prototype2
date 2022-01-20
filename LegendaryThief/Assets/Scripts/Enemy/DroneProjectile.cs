using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
      public float speed = 4f;
      public float damage = 1f;
      public float lifeTime = 2f;
      private Rigidbody2D _rb;
      // Start is called before the first frame update
      void Start()
      {
            _rb = GetComponent<Rigidbody2D>();
      }

      // Update is called once per frame
      void FixedUpdate()
      {
            _rb.velocity = transform.up * speed;
      }
      private void Update()
      {
            if(lifeTime <= 0)
            {
                  Destroy(gameObject);
            }
            lifeTime -= Time.deltaTime;
      }
      private void OnCollisionEnter2D(Collision2D collision)
      {
            if(collision.gameObject.tag == "Player")
            {
                  collision.gameObject.GetComponent<CharacterBase>().OnDamage(1, transform.position);
                  Destroy(gameObject);
            }else if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
            {
                  Destroy(gameObject);
            }
      }
}
