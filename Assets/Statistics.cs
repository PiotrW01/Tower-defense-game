using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static int[] stats = new int[7];
    public static int Kills;
    public static int MoneySpent;
    public static int StructuresBuilt;
    public static int TotalWaves;
    public static int HighestWave;
    public static int DamageDone;
    public static int DamageTaken;
    public Transform container;

    private void OnEnable()
    {
        LoadStatistics();

        var items = container.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < stats.Length; i++)
        {
            items[i * 2 + 1].text = stats[i].ToString();
        }
    }
    public static void LoadStatistics()
    {
        stats[0] = Kills = PlayerPrefs.GetInt("Kills", 0);
        stats[1] = MoneySpent = PlayerPrefs.GetInt("MoneySpent", 0);
        stats[2] = StructuresBuilt = PlayerPrefs.GetInt("StructuresBuilt", 0);
        stats[3] = TotalWaves = PlayerPrefs.GetInt("TotalWaves", 0);
        stats[4] = HighestWave = PlayerPrefs.GetInt("HighestWave", 0);
        stats[5] = DamageDone = PlayerPrefs.GetInt("DamageDone", 0);
        stats[6] = DamageTaken = PlayerPrefs.GetInt("DamageTaken", 0);

    }
}
