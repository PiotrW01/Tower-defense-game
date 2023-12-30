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
        if (CreateTurret.isPlacing) return;
        SoundManager.Instance.PlayTurretPickPlace();
        createTurret.CreateTurrett(0);
    }

    public void BuyTurret2()
    {
        if (CreateTurret.isPlacing) return;
        SoundManager.Instance.PlayTurretPickPlace();
        createTurret.CreateTurrett(1);
    }

    public void BuyTurret3()
    {
        if (CreateTurret.isPlacing) return;
        SoundManager.Instance.PlayTurretPickPlace();
        createTurret.CreateTurrett(2);
    }
}
