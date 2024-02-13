using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BaseBullet
{
    public override void OnEnemyCollision(GameObject collision)
    {
        collision.gameObject.GetComponent<baseEnemy>().DealDamageToEnemy(damage);
    }
}
