using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTurret : MonoBehaviour
{
    [HideInInspector]
    public Transform shadow;
    public GameObject bullet;
    public bool isOnCooldown = false;
    public bool canClick = false;

    protected float damageMultiplier = 1f;
    public float attackRadius;
    protected float cooldownTime;
    protected int upgradeCost;
    public TextMeshProUGUI upgradeCostText;
    public bool[] upgradeable; // dmg, cooldown, radius
    public int maxUpgradeLevel = 3;
    public int currentLevel = 0;


    //private bool infoOnTop;
    private bool showInfoWindow = false;
    private readonly int cost = 20;
    private float currentTime = 0f;
    private readonly List<enemyMovement> enemyDistanceList = new();
    private float offset;
    private GameObject infoWindow;
    private Transform closestEnemy;
    private Collider2D[] enemiesInRange;
    private Transform lufa;
    private Animation anim;

    public BaseTurret(float attackRadius, float cooldownTime, int cost, int upgradeCost, bool[] upgradeable)
    {
        this.attackRadius = attackRadius;
        this.cooldownTime = cooldownTime;
        this.cost = cost;
        this.upgradeCost = upgradeCost;
        this.upgradeable = upgradeable;
    }
    private void Awake()
    {
        attackRadius *= transform.localScale.x;
        anim = gameObject.GetComponent<Animation>();
        infoWindow = transform.Find("InfoWindow").gameObject;
        infoWindow.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
        infoWindow.SetActive(false);
        shadow = transform.Find("shadow");
        shadow.localScale = new Vector2(attackRadius, attackRadius);
        shadow.gameObject.SetActive(true);

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
    private void OnMouseOver()
    {
        if (!canClick) return;
        if (Input.GetMouseButtonDown(1))
        {
            if (anim.IsPlaying("PlaceAnimation")) return;
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
            Invoke(nameof(HideInfo), 0.3f);
        }
    }
    private void OnMouseEnter()
    {
        if (IsInvoking("HideInfo"))
        {
            showInfoWindow = true;
            CancelInvoke(nameof(HideInfo));
        }
    }
    private Transform FindClosestEnemy(Collider2D[] enemiesInRange)
    {
        foreach (var enemy in enemiesInRange)
        {
            enemy.TryGetComponent(out enemyMovement enemyMovementComponent);
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
        
        if (target == null) return;
        RotateToEnemy();

        GameObject tempBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        tempBullet.GetComponent<BaseBullet>().damage *= damageMultiplier;
        tempBullet.GetComponent<BaseBullet>().enemy = target.gameObject;
        
        isOnCooldown = true;
    }
    private void RotateToEnemy()
    {

        lufa.transform.up = closestEnemy.position - lufa.position;

/*        Vector2 direction = closestEnemy.position - lufa.position;
        float targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        lufa.rotation = Quaternion.Euler(0f, 0f, targetZ);*/
    }
    private void UpdateInfo()
    {
        var details = infoWindow.GetComponentsInChildren<TextMeshProUGUI>();
        details[0].text = "Damage: " + bullet.GetComponent<BaseBullet>().damage * damageMultiplier;
        details[1].text = "Cooldown: " + cooldownTime.ToString("0.00") + "s";
        details[2].text = "Attack range: " + attackRadius + "m";
        upgradeCostText.text = upgradeCost + "$";
    }
    public void ShowInfo()
    {
        shadow.gameObject.SetActive(true);
        infoWindow.SetActive(true);
        anim.Play("InfoAnimation");
        
    }
    public void HideInfo()
    {
        if (anim.IsPlaying("InfoAnimationHide")) return;
        // Coroutine disables the InfoWindow gameobject after the animation ends
        StartCoroutine(DisableInfoWindow());

        offset = anim["InfoAnimation"].length - anim["InfoAnimation"].time;
        anim.Stop("InfoAnimation");
        if (offset > anim["InfoAnimation"].length - 0.01f) offset = 0;
        anim["InfoAnimationHide"].time = offset;
        anim.Play("InfoAnimationHide");
        shadow.gameObject.SetActive(false);
    }
    private IEnumerator DisableInfoWindow()
    {
        yield return new WaitForSeconds(anim["InfoAnimationHide"].length);
        if(!showInfoWindow) infoWindow.SetActive(false);
    }
    private bool CanUpgrade()
    {
        if (Player.CanBuy(upgradeCost) && currentLevel <= maxUpgradeLevel)
        {
            return true;
        } return false;
    }
    public void UpgradeTurret()
    {
        if (CanUpgrade())
        {
            currentLevel++;
            Player.Buy(upgradeCost);
            upgradeCost = (int)(upgradeCost * 1.5f);
            CustomUpgrades();
            UpdateInfo();
        }
    }

    public void RemoveTurret()
    {
        Destroy(gameObject);
        Player.AddMoney(100);
    }
    protected abstract void CustomUpgrades();
    public int GetCost()
    {
        return cost;
    }
    public Animation GetAnimations()
    {
        return anim;
    }

}
