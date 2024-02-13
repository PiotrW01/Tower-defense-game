using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    public float health;
    public float speed;
    public int droppedMoney;
    public int damageToPlayer;

    public baseEnemy(float health, float speed, int droppedMoney, int damageToPlayer)
    {
        this.health = health;
        this.speed = speed;
        this.droppedMoney = droppedMoney;
        this.damageToPlayer = damageToPlayer;
    }

    private void Start()
    {
        speed *= transform.localScale.x;
    }

    public int GetDamageToPlayer()
    {
        return this.damageToPlayer;
    }
    public void DealDamageToEnemy(float dmg)
    {
        health -= dmg;
        Player.damageDone += (int)dmg;
        if(health <= 0)
        {
            Destroy(gameObject);
            Player.totalKills++;
            Player.AddMoney(droppedMoney);
        }
    }

    private void OnDestroy()
    {
        spawnEnemy.enemiesAlive--;
    }
}
