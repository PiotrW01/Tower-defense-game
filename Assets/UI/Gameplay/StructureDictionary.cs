using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDictionary : MonoBehaviour
{
    public static Dictionary<Building, GameObject> Buildings;
    public static Dictionary<Turret, GameObject> Turrets;
    public GameObject Building1;
    public GameObject Building2;
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;

    // Start is called before the first frame update
    void Awake()
    {
        Buildings = new()
        {
            { Building.Building1, Building1 },
            { Building.Building2, Building2 },
        };
        Turrets = new()
        {
            { Turret.Turret1, Turret1 },
            { Turret.Turret2, Turret2 },
            { Turret.Turret3, Turret3 },
            { Turret.Turret4, Turret4 },
        };
    }


    public enum Building
    {
        Building1,
        Building2,
    }
    public enum Turret
    {
        Turret1,
        Turret2,
        Turret3,
        Turret4,
    }
}
