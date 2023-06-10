using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnPlayerDeath : MonoBehaviour
{
    public TextMeshProUGUI totalKills;
    public TextMeshProUGUI totalMoney;
    public TextMeshProUGUI totalWaves;

    // Start is called before the first frame update
    private void OnEnable()
    {
        totalKills.text = "Total kills: " + Player.totalKills.ToString();
        totalMoney.text = "Total money spent: " + Player.totalMoneySpent.ToString();
        totalWaves.text = "Waves survived: " + Player.totalWaves.ToString();
    }
}
