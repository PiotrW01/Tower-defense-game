using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static int health = 100;
    public static int money = 220;
    public static bool isAlive = true;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        moneyText.text = money.ToString();
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        moneyText.text = money.ToString();
        healthText.text = health.ToString();
        if (health <= 0 && isAlive) gameOver();
    }

    public void gameOver()
    {
        GameObject.Find("GameEvents").gameObject.SetActive(false);
        isAlive = false;
    }


}
