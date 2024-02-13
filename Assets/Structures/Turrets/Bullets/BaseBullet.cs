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
    public float baseDamage;
    public float speed;
    [HideInInspector]
    public float aliveTime = 4f;
    [HideInInspector]
    public float damage;

    private Vector3 direction;
    private Vector3 lastEnemyPos;
    private bool isTargetDead = false;
    protected bool hasHitAnEnemy = false;
    private ParticleSystem particles;

    private void Start()
    {
        damage = baseDamage;
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (enemy == null) return;
        SoundManager.Instance.PlaySound(bulletFire);
        StartCoroutine(DisableBullet(4f));
    }

    private void OnDisable()
    {
        
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

            StopAllCoroutines();
            StartCoroutine(DisableBullet(2f));
        }
    }

    private void ResetBulletState()
    {
        isTargetDead = false;
        hasHitAnEnemy = false;
        enemy = null;
        damage = baseDamage;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        gameObject.SetActive(false);
    }

    public IEnumerator DisableBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetBulletState();
    } 

    public virtual void OnEnemyCollision(GameObject collision)
    {
        collision.gameObject.GetComponent<baseEnemy>().DealDamageToEnemy(damage);
    }
}
