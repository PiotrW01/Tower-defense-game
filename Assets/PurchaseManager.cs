using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class PurchaseManager : MonoBehaviour
{
    public LayerMask terrainMask;
    public static bool isPlacing = false;
    public static GameObject TempStructure = null;

    private void Start()
    {
        terrainMask = LayerMask.GetMask("Terrain");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            if (isPlacing)
            {
                isPlacing = false;
                Destroy(TempStructure);
                return;
            }
        }
        if (isPlacing)
        {
            HoldStructure();
            if (Input.GetMouseButton(0) && TempStructure.GetComponent<placeDetection>().canPlace 
                && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceStructure();
            }
        }
    }

    public static void CreateTempStructure(GameObject structure)
    {
        TempStructure = Instantiate(structure);
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
        // adjust for turrets
        BaseTurret baseTurret = null;
        TryGetComponent<BaseTurret>(out baseTurret);
        if(baseTurret != null)
        {

        }

        isPlacing = false;
        Destroy(TempStructure.GetComponent<placeDetection>());
        TempStructure = null;
    }
}
