using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class BaseBullet : MonoBehaviour
{
    public AudioClip bulletFire;

    [HideInInspector]
    public GameObject enemy;
    public float damage;
    public float speed;

    private Vector3 direction;
    private Vector3 lastEnemyPos;
    private float destroyTime = 8f;
    private bool isTargetDead = false;
    protected bool hasHitAnEnemy = false;
    private ParticleSystem particles;

/*    public BaseBullet(float damage, float speed, float destroyTime) 
    {
        this.damage = damage;
        this.speed = speed;
        this.destroyTime = destroyTime;
    }*/

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        SoundManager.Instance.PlaySound(bulletFire);
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (hasHitAnEnemy) return;
        // If the initial target died
        if (enemy == null)
        {
            // Keep moving straight forward
            if (!isTargetDead)
            {
                
                direction = (lastEnemyPos - transform.position).normalized;
                isTargetDead = true;
            }
            transform.position += speed * Time.deltaTime * direction;
        }
        // Else keep following the target
        else
        {
            lastEnemyPos = enemy.transform.position;
            transform.LookAt(lastEnemyPos);
            float yRot = transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, yRot, 0f);
            transform.position = Vector3.MoveTowards(transform.position, lastEnemyPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we have collided with an enemy AND if the bullet has already hit an enemy
        // AND (if the bullet has no target enemy OR it's the targeted enemy)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") 
            && !hasHitAnEnemy && (enemy == null || ReferenceEquals(enemy, collision.gameObject)))
        {
            OnEnemyCollision(collision.gameObject);
            hasHitAnEnemy = true;
            particles.Play();
            GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2f);
            //gameObject.SetActive(false);
        }
    }

    public abstract void OnEnemyCollision(GameObject collision);
}