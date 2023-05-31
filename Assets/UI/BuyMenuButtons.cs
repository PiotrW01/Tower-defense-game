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

    public void BuyTurret1()
    {
        if (createTurret.isPlacing) return;
        createTurret.CreateTurrett(0);
    }

    public void BuyTurret2()
    {
        if (createTurret.isPlacing) return;
        createTurret.CreateTurrett(1);
    }

}
