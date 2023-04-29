using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public GameObject bullet;
    public Transform childCircle;

    public bool isOnCooldown = false;
    public bool canClick = false;
    public bool showAttackRadius = false;

    private bool showInfoWindow = false;

    private int cost = 20;
    private int damageMultiplier = 1;
    private float attackRadius = 2f;
    private float cooldownTime = 0.6f;
    private float currentTime = 0f;

    private readonly List<enemyMovement> enemyDistanceList = new();
    private float offset;
    private GameObject infoWindow;
    private Transform closestEnemy;
    private Collider2D[] enemiesInRange;
    private Transform lufa;
    private Animation anim;

    public BaseTurret(float attackRadius = 2f, float cooldownTime = 1.0f, int cost = 180)
    {
        this.attackRadius = attackRadius;
        this.cooldownTime = cooldownTime;
        this.cost = cost;
    }


    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        infoWindow = transform.GetChild(2).gameObject;
        childCircle = transform.Find("shadow");
        childCircle.localScale = new Vector2(attackRadius * 4, attackRadius * 4);
        lufa = transform.Find("lufa holder");
    }

    private void Update()
    {
        if (!Player.isAlive) return;
        currentTime += Time.deltaTime;
        if(cooldownTime <= currentTime) 
        {
            currentTime = 0f;
            isOnCooldown = false;
        }
        if (!isOnCooldown)
        {
            currentTime = 0f;
            enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            if (enemiesInRange.Length != 0)
            {
                closestEnemy = FindClosestEnemy(enemiesInRange);
                FireBullet(closestEnemy);
            }
        }

    }
    private void RotateToEnemy()
    {

        lufa.transform.up = closestEnemy.position - lufa.position;

/*        Vector2 direction = closestEnemy.position - lufa.position;
        float targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        lufa.rotation = Quaternion.Euler(0f, 0f, targetZ);*/
    }

    private void OnMouseOver()
    {
        if (!canClick) return;
        if(Input.GetMouseButtonDown(0))
        {
            showAttackRadius = !showAttackRadius;
            if (showAttackRadius) childCircle.gameObject.SetActive(true);
            else childCircle.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            showInfoWindow = !showInfoWindow;
            StopAllCoroutines();

            if (showInfoWindow)
            {
                UpdateInfo();
                ShowInfo();
            } else
            {
                HideInfo();
            }
        }
    }
    private void OnMouseExit()
    {
        if(infoWindow.activeInHierarchy && !anim.IsPlaying("InfoAnimationHide"))
        {
            showInfoWindow = false;
            Invoke("HideInfo", 0.3f);
        }
    }

    private void OnMouseEnter()
    {
        if (IsInvoking("HideInfo"))
        {
            showInfoWindow = true;
            CancelInvoke("HideInfo");
        }
    }

    private IEnumerator disableInfoWindow()
    {
        yield return new WaitForSeconds(anim["InfoAnimationHide"].length);
        if(!showInfoWindow) infoWindow.SetActive(false);
    }

    private void UpdateInfo()
    {
        var details = infoWindow.GetComponentsInChildren<TextMeshProUGUI>();
        details[0].text = "Damage: " + bullet.GetComponent<BaseBullet>().damage * damageMultiplier;
        details[1].text = "Firerate: " + cooldownTime;
        details[2].text = "Attack range: " + attackRadius;
    }
    public void ShowInfo()
    {
        infoWindow.SetActive(true);
        anim.Play("InfoAnimation");
    }
    public void HideInfo()
    {
        if (anim.IsPlaying("InfoAnimationHide")) return;
        StartCoroutine(disableInfoWindow());
        offset = anim["InfoAnimation"].length - anim["InfoAnimation"].time;
        anim.Stop("InfoAnimation");
        if (offset > anim["InfoAnimation"].length - 0.01f) offset = 0;
        anim["InfoAnimationHide"].time = offset;
        anim.Play("InfoAnimationHide");
    }
    private void FireBullet(Transform target)
    {
        
        if (target == null) return;
        RotateToEnemy();

        GameObject tempBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        tempBullet.GetComponent<BaseBullet>().damage *= damageMultiplier;
        tempBullet.GetComponent<BaseBullet>().enemy = target.gameObject;
        
        isOnCooldown = true;
    }
    private Transform FindClosestEnemy(Collider2D[] enemiesInRange)
    {
        foreach (var enemy in enemiesInRange)
        {
            enemy.TryGetComponent<enemyMovement>(out enemyMovement enemyMovementComponent);
            if (enemyMovementComponent != null) enemyDistanceList.Add(enemyMovementComponent);
        }

        if (enemyDistanceList.Count > 0)
        {
            float min = enemyDistanceList[0].distance;
            float waypointIndex = enemyDistanceList[0].waypointIndex;
            int index = 0;

            for (int i = 0; i < enemyDistanceList.Count; i++)
            {
                if (enemyDistanceList[i].waypointIndex > waypointIndex ||
                    enemyDistanceList[i].distance < min && enemyDistanceList[i].waypointIndex == waypointIndex)
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
    public int getCost()
    {
        return cost;
    }
    public Animation GetAnimations()
    {
        return anim;
    }
}
