using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : BaseBullet
{
    private float explosionRadius = 1.5f;

    public GameObject explosion;


    public CannonBullet() : base(150, 10f, 3f) 
    {
        
    }

    private void Awake()
    {
        explosion.transform.localScale = new Vector2(explosionRadius * 2, explosionRadius * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we have collided with an enemy AND if the bullet has already hit an enemy
        // AND (if the bullet has no target enemy OR it's the targeted enemy)
        if (collision.gameObject.CompareTag("enemy") && !hasHitAnEnemy
            && (enemy == null || ReferenceEquals(enemy, collision.gameObject)))
        {
            hasHitAnEnemy = true;
            var enemies = Physics2D.OverlapCircleAll(collision.gameObject.transform.position, explosionRadius);

            foreach(var enemy in enemies)
            {
                enemy.TryGetComponent(out baseEnemy enemyComponent);
                if (enemyComponent != null)
                {
                    enemyComponent.DealDamageToEnemy(damage);
                }
            }
            var explosionObject = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(explosionObject, 0.3f);
            Destroy(gameObject);
        }
    }

}
