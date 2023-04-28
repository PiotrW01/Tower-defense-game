using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuyMenuButtons : MonoBehaviour
{

    private CreateTurret createTurret;

    private void Start()
    {
        createTurret = GameObject.Find("GameEvents").GetComponent<CreateTurret>();
    }

    public void buyTurret1()
    {
        if (createTurret.isPlacing) return;
        createTurret.createTurret(0);
    }

    public void buyTurret2()
    {
        if (createTurret.isPlacing) return;
        createTurret.createTurret(1);
    }

}
