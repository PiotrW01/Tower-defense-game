using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PurchaseManager : MonoBehaviour
{
    public LayerMask terrainMask;
    public static bool isPlacing = false;
    public static GameObject TempStructure = null;
    public static int tempPrice = 0;

    private void Start()
    {
        terrainMask = LayerMask.GetMask("Terrain");
    }

    void Update()
    {
        if (!isPlacing) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            isPlacing = false;
            Destroy(TempStructure);
            return;
        }

        TempStructure.TryGetComponent<Turret>(out var turret);
        if (turret != null)
        {
            if (!TempStructure.GetComponent<placeDetection>().canPlace)
            {
                var radius = turret.radius.transform.GetComponent<SpriteRenderer>();
                radius.color = new Color(1f, 0f, 0f, 0.4f);
            } else
            {
                var radius = turret.radius.transform.GetComponent<SpriteRenderer>();
                radius.color = new Color(1f, 1f, 1f, 0.4f);
            }
        }
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() &&
            TempStructure.GetComponent<placeDetection>().canPlace)
        {
            PlaceStructure();
            return;
        }
        HoldStructure();
    }

    public static void CreateTempStructure(GameObject structure, int price)
    {
        SoundManager.Instance.PlayButtonClick();
        tempPrice = price;
        TempStructure = Instantiate(structure, GameObject.Find("Player").transform);
        isPlacing = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            TempStructure.transform.position = hit.point;
        }
        else TempStructure.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7));
    }
    public void HoldStructure()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainMask))
        {
            TempStructure.transform.position = hit.point;
        }
    }

    public void PlaceStructure()
    {
        SoundManager.Instance.PlayButtonClick();
        if (!Player.CanBuy(tempPrice)) return;
        Player.Buy(tempPrice);
        tempPrice = 0;

        TempStructure.TryGetComponent<Turret>(out var turret);
        if(turret != null)
        {
            turret.EnableTurret();
            turret.radius.SetActive(false);
        }

        isPlacing = false;
        Destroy(TempStructure.GetComponent<placeDetection>());
        TempStructure = null;
        Player.structuresBuilt++;
    }
}
