using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{
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

        money = MapPreview.ChosenMapData.playerStartingMoney;
        health = 100;

        isAlive = true;
        totalWaves = 0;
        totalKills = 0;
        totalMoneySpent = 0;


        healthText.text = health.ToString();
        moneyText.text = money.ToString();
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    public static void GameOver()
    {
        isAlive = false;
        GameObject.Find("EventSystem").SetActive(false);
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

}
