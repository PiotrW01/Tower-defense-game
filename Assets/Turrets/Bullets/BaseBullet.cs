using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseBullet : MonoBehaviour
{
    public AudioClip bulletFire;

    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    private float speed;

    private Vector3 direction;
    private Vector3 lastEnemyPos;
    private Vector3 initialPos;
    private float destroyTime = 3f;
    private bool isTargetDead = false;
    protected bool hasHitAnEnemy = false;

    public BaseBullet(float damage, float speed, float destroyTime) 
    {
        this.damage = damage;
        this.speed = speed;
        this.destroyTime = destroyTime;
    }

    private void Start()
    {
        SoundManager.Instance.PlaySound(bulletFire);
        Destroy(gameObject, destroyTime);
        initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        // If the initial target died
        if (enemy == null)
        {
            // Keep moving straight forward
            if (!isTargetDead)
            {
                
                direction = (lastEnemyPos - initialPos).normalized;
                isTargetDead = true;
            }
            transform.position += speed * Time.deltaTime * direction;
        }
        // Else keep following the target
        else
        {
            lastEnemyPos = enemy.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, lastEnemyPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we have collided with an enemy AND if the bullet has already hit an enemy
        // AND (if the bullet has no target enemy OR it's the targeted enemy)
        if (collision.gameObject.CompareTag("enemy") && !hasHitAnEnemy 
            && (enemy == null || ReferenceEquals(enemy, collision.gameObject)))
        {
            collision.gameObject.GetComponent<baseEnemy>().DealDamageToEnemy(damage);
            hasHitAnEnemy = true;
            Destroy(gameObject);
        }
    }




}
