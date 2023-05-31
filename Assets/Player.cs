using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static int health = 100;
    public static int money = 2200;
    public static bool isAlive = true;
    public static TextMeshProUGUI moneyText;
    public static TextMeshProUGUI healthText;

    private void Start()
    {
        moneyText.text = money.ToString();
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    public static void GameOver()
    {
        GameObject.Find("GameEvents").SetActive(false);
        isAlive = false;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            healthText.text = health.ToString();
            GameOver();
        }
    }

    public static bool CanBuy(int cost)
    {
        if (money - cost < 0) return false;
        else
        {
            money -= cost;
            moneyText.text = money.ToString();
            return true;
        }
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }

}
