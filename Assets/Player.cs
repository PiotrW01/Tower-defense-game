using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TerrainUtils;
using static StructureDictionary;


public class Player : MonoBehaviour
{
    private Transform selectedStructure = null;
    public GameObject MapPrefab;
    public static int health;
    public static int money;
    public static bool isAlive;
    public static int totalKills;
    public static int totalMoneySpent;
    public static int totalWaves;

    private static TextMeshProUGUI moneyText;
    private static TextMeshProUGUI healthText;

    private void Start()
    {
        Instantiate(MapPrefab);
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        GameObject.Find("UserInterface").transform.Find("GameOverInterface").gameObject.SetActive(false);

        if(MapPreview.ChosenMapData != null) money = MapPreview.ChosenMapData.playerStartingMoney;
        health = 100;

        isAlive = true;
        totalWaves = 0;
        totalKills = 0;
        totalMoneySpent = 0;

        healthText.text = health.ToString();
        moneyText.text = money.ToString();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(1)) // Toggle radius
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hit.collider.TryGetComponent<Turret>(out var turret);
                if (turret == null)
                {
                    return;
                }
                turret.ToggleTurretRadius();
            }
        } else if (Input.GetMouseButtonDown(0)) // Togle structure info
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hit.collider.TryGetComponent<StructureInfo>(out var info);
                if (info == null)
                {
                    return;
                }
                info.ToggleInfo();
            }
        }
    }

    public static void GameOver()
    {
        isAlive = false;
        SaveGameStatistics();
        GameObject.Find("UserInterface").transform.Find("GameOverInterface").gameObject.SetActive(true);
    }

    public static void DamagePlayer(int damage)
    {
        if (!isAlive) return;
        health -= damage;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            health = 0;
            healthText.text = "0";
            GameOver();
        }
    }

    public static void SaveGameStatistics()
    {
        if(PlayerPrefs.GetInt("MaxWavesSurvived", 0) < totalWaves)
        {
            PlayerPrefs.SetInt("MaxWavesSurvived", totalWaves);
        }

        int overallMoneySpent = PlayerPrefs.GetInt("overallMoneySpent", 0);
        PlayerPrefs.SetInt("overallMoneySpent", totalMoneySpent + overallMoneySpent);

        int overallKills = PlayerPrefs.GetInt("overallKills", 0);
        PlayerPrefs.SetInt("overallKills", overallKills + totalKills);

        PlayerPrefs.Save();
    }

    public static bool CanBuy(int cost)
    {
        if (money - cost < 0) return false;
        else return true;
    }

    public static void Buy(int cost)
    {
        money -= cost;
        totalMoneySpent += cost;
        moneyText.text = money.ToString();
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }

    public static int CalculateScore()
    {
        return totalKills * 2 + totalWaves * 2;
    }
}
