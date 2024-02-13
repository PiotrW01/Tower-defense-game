using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float attackRadius;
    public float cooldown;
    public int upgradeCost;
    public float damageMultiplier = 2f;
    public float cooldownMultiplier = 1.2f;
    public float radiusMultiplier = 1.2f;
    public int maxLevel = 3;
    public int currentLevel = 0;

    private new Animation animation;
    private List<EnemyMovement2> enemyDistanceList = new();
    private List<GameObject> bulletPool;
    private Transform closestEnemy;
    private Collider[] enemiesInRange;
    public GameObject radius;

    private void Awake()
    {
        animation = GetComponentInChildren<Animation>();
        animation[animation.clip.name].speed = 1 / cooldown;
        attackRadius *= transform.localScale.x;
        radius = Instantiate(radius, transform);
        radius.transform.localScale = new Vector3(attackRadius * 2, attackRadius * 2, attackRadius * 2);
    }

    private void Update()
    {
        if (closestEnemy == null) return;
        transform.LookAt(closestEnemy);
        float yRot = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

    private Transform FindClosestEnemy(Collider[] enemiesInRange)
    {
        if(enemiesInRange.Length == 0) return null;
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
        bullet.GetComponent<BaseBullet>().damage *= damageMultiplier;
        bullet.transform.localPosition = new Vector3(0, 0.62f, 0f);

        animation.Play();
        bullet.SetActive(true);
    }
    public void ToggleTurretRadius()
    {
        if (radius.activeInHierarchy)
        {
            radius.SetActive(false);
        }
        else
        {
            radius.SetActive(true);
            radius.transform.localScale = new Vector3(attackRadius * 2, attackRadius * 2, attackRadius * 2);
        }
    }
    private bool CanUpgrade()
    {
        if (Player.CanBuy(upgradeCost) && currentLevel < maxLevel)
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
            attackRadius = attackRadius * radiusMultiplier;
            cooldown = cooldown / cooldownMultiplier;
            damageMultiplier *= 1.2f;
            animation[animation.clip.name].speed = 1 / cooldown;
            ResizeBulletPool();
        }
    }
    public void EnableTurret()
    {
        bulletPool = new();
        GameObject obj;
        float bulletAliveTime = BulletPrefab.GetComponent<BaseBullet>().aliveTime;
        int maxBullets = (int)(bulletAliveTime * (1 / cooldown)) + 1;

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
        LayerMask mask = LayerMask.GetMask("Enemy");
        while (Player.isAlive)
        {
            enemiesInRange = Physics.OverlapSphere(transform.position, attackRadius, mask);
            closestEnemy = FindClosestEnemy(enemiesInRange);

            if (closestEnemy != null)
            {
                FireBullet(closestEnemy);
                yield return new WaitForSeconds(cooldown);
            }
            yield return null;
        }
    }

    private void ResizeBulletPool()
    {
        float bulletAliveTime = BulletPrefab.GetComponent<BaseBullet>().aliveTime;
        int maxBullets = (int)(bulletAliveTime * (1 / cooldown)) + 1;
        if (maxBullets <= bulletPool.Count) return;
        GameObject obj;

        for (int i = bulletPool.Count; i < maxBullets; i++)
        {
            obj = Instantiate(BulletPrefab, transform);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.GetComponent<placeDetection>()) return;
        Player.AddMoney(100);
    }
}
