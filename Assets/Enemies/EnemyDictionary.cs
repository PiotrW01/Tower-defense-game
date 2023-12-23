using System.Collections.Generic;
using UnityEngine;

public class EnemyDictionary : MonoBehaviour
{
    public static Dictionary<Enemy, GameObject> Enemies;
    public GameObject APC_B1;
    public GameObject APC_B2;
    public GameObject APC_B3;
    public GameObject APC_B4;
    public GameObject APC_BOSS;

    // Start is called before the first frame update
    void Awake()
    {
        Enemies = new()
        {
            {Enemy.APC_B1, APC_B1},
            {Enemy.APC_B2, APC_B2},
            {Enemy.APC_B3, APC_B3},
            {Enemy.APC_B4, APC_B4},
            {Enemy.APC_BOSS, APC_BOSS},
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
    APC_B3,
    APC_B4,
    APC_BOSS,
}