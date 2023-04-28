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
            holdTurret();
            if (Input.GetMouseButton(0) && turretTemp.GetComponent<placeDetection>().canPlace)
            {
                if(placeTurret()) isPlacing = false;
            }
        }
    }


    private void holdTurret()
    {
        turretTemp.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
    }

    public void createTurret(int id)
    {
        if (turretTemp != null) return;
        isPlacing = true;
        turretTemp = Instantiate(turrets[id],
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
            Quaternion.identity);
        turretTemp.GetComponent<BaseTurret>().enabled = false;
    }

    private bool placeTurret()
    {
        var t = turretTemp.GetComponent<BaseTurret>();

        if (Player.money < t.getCost()) return false;
        Player.money -= t.getCost();

        t.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        t.enabled = true;
        t.canClick = true;
        t.childCircle.gameObject.SetActive(false);
        t.GetAnimations().Play("PlaceAnimation");
        turretTemp.GetComponent<SpriteRenderer>().sortingLayerID = 0;
        turretTemp.transform.GetComponentInChildren<SpriteRenderer>().sortingLayerID = 0;
        turretTemp.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().sortingLayerID = 0;


        turretTemp = null;
        return true;
    }

}
