using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building2 : MonoBehaviour
{
    public int healthPerTick = 1;
    public float cooldown = 10f;

    public void Start()
    {
        StartCoroutine(RegenHealth());
    }

    IEnumerator RegenHealth()
    {
        while (Player.isAlive)
        {
            if (spawnEnemy.enemiesAlive > 0)
            {
                yield return new WaitForSeconds(cooldown);
                Player.health += healthPerTick;
            }
            else yield return null;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.GetComponent<placeDetection>()) return;
        Player.AddMoney(100);
    }
}
