using System.Collections.Generic;
using UnityEngine;

public class EnemyDictionary : MonoBehaviour
{
    public static Dictionary<Enemy, GameObject> Enemies;
    public GameObject APC_B1;
    public GameObject APC_B2;
    public GameObject APC_PLAZ1;
    public GameObject APC_PLAZ2;
    public GameObject BAGI_B1;
    public GameObject BAGI_ROCKET1;

    // Start is called before the first frame update
    void Awake()
    {
        Enemies = new()
        {
            {Enemy.APC_B1, APC_B1},
            {Enemy.APC_B2, APC_B2},
            {Enemy.APC_PLAZ1, APC_PLAZ1},
            {Enemy.APC_PLAZ2, APC_PLAZ2},
            {Enemy.BAGI_B1, BAGI_B1},
            {Enemy.BAGI_ROCKET1, BAGI_ROCKET1 },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject Get(Enemy enemy)
    {
        return Enemies[enemy];
    }
}

public enum Enemy
{
    APC_B1,
    APC_B2,
    APC_PLAZ1,
    APC_PLAZ2,
    BAGI_B1,
    BAGI_ROCKET1,
}