using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    private int health;
    private readonly float speed;
    private readonly int droppedMoney;
    private readonly int damageToPlayer;

    public baseEnemy(int health, float speed, int droppedMoney, int damageToPlayer)
    {
        this.health = health;
        this.speed = speed;
        this.droppedMoney = droppedMoney;
        this.damageToPlayer = damageToPlayer;
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


    public float GetSpeed()
    {
        return this.speed;
    }

    public int GetDamageToPlayer()
    {
        return this.damageToPlayer;
    }

    public int GetHealth()
    {
        return health;
    }

    public void DealDamageToEnemy(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Destroy(gameObject);
            Player.AddMoney(droppedMoney);
        }
    }

}
