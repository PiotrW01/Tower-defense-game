using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour
{
    //private Transform selectedStructure = null;
    public GameObject MapPrefab;
    public static int health;
    public static int money;
    public static int score = 0;
    public static bool isAlive;
    public static int totalKills = 0;
    public static int moneySpent = 0;
    public static int waves = 0;
    public static int damageTaken = 0;
    public static int damageDone = 0;
    public static int structuresBuilt = 0;

    private static TextMeshProUGUI moneyText;
    private static TextMeshProUGUI healthText;

    private void Start()
    {
        Instantiate(MapPrefab);
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        GameObject.Find("UserInterface").transform.Find("GameOverInterface").gameObject.SetActive(false);

        if(MapPreview.ChosenMapData != null) money = MapPreview.ChosenMapData.playerStartingMoney;
        money = 9999;
        health = 10;
        isAlive = true;
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
        } else if (Input.GetMouseButtonDown(0)) // Toggle structure info
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Structure") && 
                    !hit.collider.GetComponent<placeDetection>())
                {
                    ToggleInfo(hit.collider.gameObject);
                } else
                {
                    ToggleInfo(null);
                }
            }
        }
    }

    public static void GameOver()
    {
        isAlive = false;
        score = CalculateScore();
        SaveGameStatistics();
        GameObject.Find("UserInterface").transform.Find("GameOverInterface").gameObject.SetActive(true);
    }

    public static void DamagePlayer(int damage)
    {
        if (!isAlive) return;
        health -= damage;
        healthText.text = health.ToString();
        damageTaken += damage;

        if (health <= 0)
        {
            health = 0;
            healthText.text = "0";
            GameOver();
        }
    }

    public static void SaveGameStatistics()
    {
        int overallMoneySpent = PlayerPrefs.GetInt("MoneySpent", 0);
        PlayerPrefs.SetInt("MoneySpent", moneySpent + overallMoneySpent);

        int overallKills = PlayerPrefs.GetInt("Kills", 0);
        PlayerPrefs.SetInt("Kills", overallKills + totalKills);

        int totalWaves = PlayerPrefs.GetInt("TotalWaves", 0);
        PlayerPrefs.SetInt("TotalWaves", totalWaves + waves);

        int overallStructuresBuilt = PlayerPrefs.GetInt("StructuresBuilt", 0);
        PlayerPrefs.SetInt("StructuresBuilt", overallStructuresBuilt + structuresBuilt);

        int highestWave = PlayerPrefs.GetInt("HighestWave", 0);
        if(highestWave < waves)
        PlayerPrefs.SetInt("HighestWave", waves);

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
        moneySpent += cost;
        moneyText.text = money.ToString();
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
    public static void ToggleInfo(GameObject structure)
    {
        GameObject infoInterface = GameObject.Find("UserInterface").transform.Find("InfoInterface").gameObject;
        if (structure == null)
        {
            infoInterface.SetActive(false);
            return;
        }
        StructureInfo info = infoInterface.GetComponent<StructureInfo>();

        SoundManager.Instance.PlayTurretInfo();
        if (infoInterface.activeInHierarchy)
        {
            if (info.selectedStructure.Equals(structure))
            {
                infoInterface.SetActive(false);
            } else
            {
                info.selectedStructure = structure;
                info.UpdateStats();
            }

        } else
        {
            info.selectedStructure = structure;
            infoInterface.SetActive(true);
        }
    }
    public static int CalculateScore()
    {
        return totalKills * 2 + waves * 2;
    }

}
