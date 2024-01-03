using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInfoWindow : MonoBehaviour
{
    public bool isMouseOverInfoWindow = false;


    private void OnMouseEnter()
    {
        isMouseOverInfoWindow = true;
    }

    private void OnMouseExit()
    {
        isMouseOverInfoWindow = false;
    }



}
