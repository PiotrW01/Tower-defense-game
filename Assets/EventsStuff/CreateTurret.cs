using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CreateTurret : MonoBehaviour
{
    public bool isPlacing = false;
    public GameObject[] turrets;
    private GameObject turretTemp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            if (isPlacing)
            {
                isPlacing = false;
                Destroy(turretTemp);
                return;
            }
/*            createTurret();
            isPlacing = true;*/
        }

        if (isPlacing)
        {
            HoldTurret();
            if (Input.GetMouseButton(0) && turretTemp.GetComponent<placeDetection>().canPlace)
            {
                PlaceTurret();
            }
        }
    }


    private void HoldTurret()
    {
        turretTemp.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
    }

    public void CreateTurrett(int id)
    {
        if (turretTemp != null || !Player.isAlive) return;
        if (Player.CanBuy(turrets[id].GetComponent<BaseTurret>().GetCost()))
        {
            isPlacing = true;
            turretTemp = Instantiate(turrets[id],
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                Quaternion.identity);
            turretTemp.GetComponent<BaseTurret>().enabled = false;
        }
    }

    private void PlaceTurret()
    {
        var t = turretTemp.GetComponent<BaseTurret>();
        Player.Buy(t.GetCost());

        t.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        t.enabled = true;
        t.canClick = true;
        t.shadow.gameObject.SetActive(false);
        t.GetAnimations().Play("PlaceAnimation");

        Destroy(turretTemp.GetComponent<placeDetection>());
        turretTemp.GetComponent<SpriteRenderer>().sortingLayerID = 0;
        turretTemp.transform.Find("lufa holder").GetComponentInChildren<SpriteRenderer>().sortingLayerID = 0;
        turretTemp.transform.Find("lufa holder").GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;

        isPlacing = false;
        turretTemp = null;
    }

}
