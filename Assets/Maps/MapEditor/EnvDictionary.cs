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

    public static Dictionary<Env, GameObject> Objects;

    private void Start()
    {
        Objects = new Dictionary<Env, GameObject>
        {
            { Env.LargeRock1, LargeRock1 },
            { Env.LargeRock2, LargeRock2 },
            { Env.SmallRock1, SmallRock1 },
            { Env.SmallRock2, SmallRock2 },
            { Env.SmallRock3, SmallRock3 },
            { Env.SmallRock4, SmallRock4 },
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
}