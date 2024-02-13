using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StructureInfo : MonoBehaviour
{
    public GameObject selectedStructure;
    private Turret turret;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI cooldown;
    public TextMeshProUGUI radius;
    public Button upgradeButton;

    private void Start()
    {
        gameObject.SetActive(false);
        upgradeButton.onClick.AddListener(delegate ()
        {
            SoundManager.Instance.PlayButtonClick();
        });
    }

    private void OnEnable()
    {
        if (selectedStructure == null) return;
        selectedStructure.TryGetComponent(out turret);
        if (turret != null)
        {
            ShowStats();
        } else
        {
            upgradeButton.interactable = false;
            damage.text = "-";
            cooldown.text = "-";
            radius.text = "-";
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }
    }


    private void OnDisable()
    {
        selectedStructure = null;
        turret = null;
    }

    public void ShowStats()
    {
        damage.text = turret.damageMultiplier.ToString("0.00");
        cooldown.text = turret.cooldown.ToString("0.00");
        radius.text = turret.radiusMultiplier.ToString("0.00");
        upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade " + turret.upgradeCost + "$";

        if (turret.currentLevel >= turret.maxLevel)
        {
            damage.color = Color.white;
            cooldown.color = Color.white;
            radius.color = Color.white;
            upgradeButton.interactable = false;
            return;
        }
        upgradeButton.interactable = true;
        damage.color = Color.green;
        cooldown.color = Color.green;
        radius.color = Color.green;
    }

    public void UpdateStats()
    {
        OnEnable();
    }

    public void Upgrade()
    {
        turret.UpgradeTurret();
        damage.text = turret.damageMultiplier.ToString("0.00");
        cooldown.text = turret.cooldown.ToString("0.00");
        radius.text = turret.attackRadius.ToString("0.00");
        upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade " + turret.upgradeCost + "$";
    }

    public void SellStruct()
    {
        SoundManager.Instance.PlayTurretDestroy();
        Destroy(selectedStructure);
        gameObject.SetActive(false);
    }
}
