using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building1 : MonoBehaviour
{

    public int moneyPerTick = 20;
    public float cooldown = 5f;


    public void Start()
    {
        StartCoroutine(FarmMoney());
    }

    IEnumerator FarmMoney()
    {
        while (Player.isAlive)
        {
            if (spawnEnemy.enemiesAlive > 0)
            {
                Player.AddMoney(moneyPerTick);
            }
            yield return new WaitForSeconds(cooldown);
        }
        yield return null;
    }

    private void OnDestroy()
    {
        if (gameObject.GetComponent<placeDetection>()) return;
        Player.AddMoney(100);
    }
}
