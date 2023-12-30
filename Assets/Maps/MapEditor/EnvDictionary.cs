using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvDictionary : MonoBehaviour
{
    public GameObject LargeRock1;
    public GameObject LargeRock2;
    public GameObject SmallRock1;
    public GameObject SmallRock2;
    public GameObject SmallRock3;
    public GameObject SmallRock4;
    public GameObject Tree1;
    public GameObject Tree2;
    public GameObject Tree3;
    public GameObject Tree4;
    public GameObject Foliage1;
    public GameObject Foliage2;
    public GameObject Foliage3;
    public GameObject GrassPatchLarge;
    public GameObject GrassPatchSmall;

    public static Dictionary<Env, GameObject> Objects;

    private void Awake()
    {
        Objects = new Dictionary<Env, GameObject>
        {
            { Env.LargeRock1, LargeRock1 },
            { Env.LargeRock2, LargeRock2 },
            { Env.SmallRock1, SmallRock1 },
            { Env.SmallRock2, SmallRock2 },
            { Env.SmallRock3, SmallRock3 },
            { Env.SmallRock4, SmallRock4 },
            { Env.Tree1, Tree1 },
            { Env.Tree2, Tree2 },
            { Env.Tree3, Tree3 },
            { Env.Tree4, Tree4 },
            { Env.Foliage1, Foliage1 },
            { Env.Foliage2, Foliage2 },
            { Env.Foliage3, Foliage3 },
            { Env.GrassPatchLarge, GrassPatchLarge },
            { Env.GrassPatchSmall, GrassPatchSmall },
        };
    }
}


public enum Env
{
    LargeRock1,
    LargeRock2,
    SmallRock1,
    SmallRock2,
    SmallRock3,
    SmallRock4,
    Tree1,
    Tree2,
    Tree3,
    Tree4,
    Foliage1,
    Foliage2,
    Foliage3,
    GrassPatchLarge,
    GrassPatchSmall,
}