using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StructureDictionary;

public class Turret : MonoBehaviour
{
    private LayerMask mask;
    public GameObject BulletPrefab;
    public bool isDoubleBarrel = true;
    public float damageMultiplier = 1f;
    public float attackRadius;
    public float cooldownTime;
    public int upgradeCost;
    public int dmgUpgradeMultiplier = 1;
    public int cooldownUpgradeMultiplier = 1;
    public int radiusUpgradeMultiplier = 1;
    public int maxUpgradeLevel = 3;
    public int currentLevel = 0;

    private List<EnemyMovement2> enemyDistanceList = new();
    private List<GameObject> bulletPool;
    private Transform closestEnemy;
    private Collider[] enemiesInRange;
    public GameObject radius;

    private void Awake()
    {
        mask = LayerMask.GetMask("Enemy");
        attackRadius *= transform.localScale.x;
        radius = Instantiate(radius, transform);
    }

    private void Update()
    {
        radius.transform.localScale = new Vector3(attackRadius * 2, attackRadius * 2, attackRadius * 2);
        if (!Player.isAlive) return;
        enemiesInRange = Physics.OverlapSphere(transform.position, attackRadius, mask);
        if(enemiesInRange.Length != 0)
        {
            closestEnemy = FindClosestEnemy(enemiesInRange);
            Transform canon = transform.Find("Canon");
            canon.LookAt(closestEnemy);
            float yRot = canon.transform.eulerAngles.y;
            canon.rotation = Quaternion.Euler(0f, yRot, 0f);
        }
    }




    private Transform FindClosestEnemy(Collider[] enemiesInRange)
    {
        foreach (var enemy in enemiesInRange)
        {
            enemy.TryGetComponent(out EnemyMovement2 enemyMovementComponent);
            if (enemyMovementComponent != null) enemyDistanceList.Add(enemyMovementComponent);
        }

        // If there are any enemies in range
        if (enemyDistanceList.Count > 0)
        {
            float min = enemyDistanceList[0].distance;
            float waypointIndex = enemyDistanceList[0].waypointIndex;
            int index = 0;

            for (int i = 0; i < enemyDistanceList.Count; i++)
            {
                // Checking if the i'th enemy is closer to the end than the current closest enemy
                // by comparing which waypoint index they are at
                // or if the waypoint index is the same, which one is closer to the next waypoint
                if (enemyDistanceList[i].waypointIndex > waypointIndex ||
                    (enemyDistanceList[i].distance < min && enemyDistanceList[i].waypointIndex == waypointIndex))
                {
                    min = enemyDistanceList[i].distance;
                    waypointIndex = enemyDistanceList[i].waypointIndex;
                    index = i;
                }
            }

            Transform closestEnemy = enemyDistanceList[index].transform;
            enemyDistanceList.Clear();
            return closestEnemy;
        }
        return null;
    }
    private void FireBullet(Transform target)
    {
        GameObject bullet = GetPooledBullet();
        if(bullet == null) return;
        bullet.GetComponent<BaseBullet>().enemy = target.gameObject;
        if (isDoubleBarrel)
        {

        } else
        {
            bullet.transform.localPosition = new Vector3(0, 0.62f, 0f);
        }

        bullet.SetActive(true);
    }
    public void ToggleTurretRadius()
    {
        if (radius.activeInHierarchy)
        {
            radius.SetActive(false);
        }
        else radius.SetActive(true);
    }
    private bool CanUpgrade()
    {
        if (Player.CanBuy(upgradeCost) && currentLevel <= maxUpgradeLevel)
        {
            return true;
        }
        return false;
    }
    public void UpgradeTurret()
    {
        if (CanUpgrade())
        {
            currentLevel++;
            Player.Buy(upgradeCost);
            upgradeCost = (int)(upgradeCost * 1.5f);
        }
    }
    public void RemoveTurret()
    {
        Destroy(gameObject);
        Player.AddMoney(120);
    }
    public void EnableTurret()
    {
        bulletPool = new();
        GameObject obj;
        float bulletAliveTime = BulletPrefab.GetComponent<BaseBullet>().aliveTime;
        int maxBullets = (int)(bulletAliveTime * (1 / cooldownTime)) + 1;

        for (int i = 0; i < maxBullets; i++)
        {
            obj = Instantiate(BulletPrefab, transform);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }

        StartCoroutine(Shoot());
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            if (!Player.isAlive) yield break;
            if (closestEnemy != null)
            {
                FireBullet(closestEnemy);
                yield return new WaitForSeconds(cooldownTime);
            }
            yield return null;
        }
    }
}
