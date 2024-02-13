using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyStructureScript : MonoBehaviour
{
    public GameObject structurePrefab;
    public TextMeshProUGUI priceText;
    private int price;
    // Start is called before the first frame update
    void Start()
    {
        structurePrefab.TryGetComponent<Price>(out var priceComponent);
        if (priceComponent == null) return;
        price = priceComponent.price;

        var button = GetComponent<Button>();
        priceText.text = price.ToString();

        button.onClick.AddListener(() =>
        {
            if (PurchaseManager.isPlacing || !Player.CanBuy(price)) return;
            PurchaseManager.CreateTempStructure(structurePrefab, price);
        });
    }

    
}
