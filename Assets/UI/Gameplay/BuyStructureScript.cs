using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyStructureScript : MonoBehaviour
{
    public int price = 100;
    public GameObject structurePrefab;
    public TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        priceText.text = price.ToString();

        button.onClick.AddListener(() =>
        {
            if (PurchaseManager.isPlacing) return;
            PurchaseManager.CreateTempStructure(structurePrefab);
        });
    }

    
}
