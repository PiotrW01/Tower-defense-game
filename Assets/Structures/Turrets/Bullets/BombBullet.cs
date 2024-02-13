using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : BaseBullet
{
    public float explosionRadius = 1.5f;

    public override void OnEnemyCollision(GameObject collision)
    {
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        foreach (var collider in colliders)
        {
            collider.GetComponent<baseEnemy>().DealDamageToEnemy(damage);
        }
    }
}
