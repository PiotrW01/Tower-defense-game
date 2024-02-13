using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Turret turret;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI cooldown;
    public TextMeshProUGUI radius;

    private string damageTemp;
    private string cooldownTemp;
    private string radiusTemp;

    private void Start()
    {
        turret = transform.parent.parent.parent.GetComponent<Turret>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
