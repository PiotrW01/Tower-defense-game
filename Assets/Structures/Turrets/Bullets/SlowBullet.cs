using UnityEngine;

public class SlowBullet : BaseBullet
{
    public float slowMultiplier = 0.5f;

    public override void OnEnemyCollision(GameObject collision)
    {
        collision.GetComponent<EnemyMovement2>().speedModifier = slowMultiplier;
        base.OnEnemyCollision(collision);
    }

}
    
