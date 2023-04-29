using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    public int health;
    private float speed;
    private int droppedMoney;
    private int damageToPlayer;

    public baseEnemy(int health = 100, float speed = 10f, int droppedMoney = 2, int damageToPlayer = 5)
    {
        this.health = health;
        this.speed = speed;
        this.droppedMoney = droppedMoney;
        this.damageToPlayer = damageToPlayer;
    }

    private void Start()
    {
        
    }


    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            var gm = collision.gameObject.GetComponent<bullet>();
            if (collision.gameObject.CompareTag("bullet") && !gm.hitEnemy && (gm.enemy == null || GameObject.ReferenceEquals(gm.enemy, gameObject)))
            {

                Debug.Log("hit");
                int damage = gm.damage;
                health -= damage;
                if (health <= 0)
                {
                    Destroy(gameObject);
                    Player.money += droppedMoney;
                }
                gm.hitEnemy = true;
                Destroy(collision.gameObject);
            }
        }*/

    private void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            Player.money += droppedMoney;
        }
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public int getDamageToPlayer()
    {
        return this.damageToPlayer;
    }
}
