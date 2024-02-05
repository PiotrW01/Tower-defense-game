using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BaseTurret turret;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI cooldown;
    public TextMeshProUGUI radius;

    private string damageTemp;
    private string cooldownTemp;
    private string radiusTemp;

    private void Start()
    {
        turret = transform.parent.parent.parent.GetComponent<BaseTurret>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        damageTemp = damage.text;
        cooldownTemp = cooldown.text;
        radiusTemp = radius.text;

        if (!(turret.currentLevel < turret.maxUpgradeLevel))
        {
            damage.color = Color.white;
            cooldown.color = Color.white;
            radius.color = Color.white;
            GetComponent<Button>().interactable = false;
            return;
        }

        if (turret.upgradeable[0])
        {
            damage.color = Color.green;
            damage.text += " +";
        }
        if (turret.upgradeable[1])
        {
            cooldown.color = Color.green;
            cooldown.text += " -";
        }
        if (turret.upgradeable[2])
        {
            radius.color = Color.green;
            radius.text += " +";
        }
    }

    public void ReAddSigns()
    {
        OnPointerEnter(null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        damage.text = damageTemp;
        cooldown.text = cooldownTemp;
        radius.text = radiusTemp;

        damage.color = Color.white; 
        cooldown.color = Color.white; 
        radius.color = Color.white;
    }
}
