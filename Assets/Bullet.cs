using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    private Vector2 direction;
    private float speed;

    public void Init(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
        Destroy(gameObject, lifeTime);
        Debug.Log("Bullet fired");
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // does a third of the enemys health when colliding with the enemy
            EnemyHealth health = collision.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(1);
            Debug.Log("Collided with enemy");
            
            Destroy(gameObject);
        }
    }
}