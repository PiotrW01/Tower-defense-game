using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseBullet : MonoBehaviour
{

    public int damage = 50;
    public float speed = 7f;
    public bool hitEnemy = false;
    public float destroyTime = 3f;

    private Vector3 direction;
    private Vector3 lastEnemyPos;
    private Vector3 initialPos;
    private bool hasActivated = false;

    public GameObject enemy;

    public BaseBullet(int damage = 50, float speed = 7f, float destroyTime = 3f) 
    {
        this.damage = damage;
        this.speed = speed;
        this.destroyTime = destroyTime;
    }

    private void Start()
    {
        Destroy(gameObject, destroyTime);
        initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (enemy == null)
        {
            if (!hasActivated)
            {
                direction = (lastEnemyPos - initialPos).normalized;
                hasActivated = true;
            }
            transform.position += speed * Time.deltaTime * direction;
        }
        else
        {
            lastEnemyPos = enemy.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, lastEnemyPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var gm = collision.gameObject.GetComponent<baseEnemy>();
        if (collision.gameObject.CompareTag("enemy") && !hitEnemy && (enemy == null || GameObject.ReferenceEquals(enemy, gm.gameObject)))
        {
            gm.health -= damage;
            hitEnemy = true;
            Destroy(gameObject);
        }
    }




}
