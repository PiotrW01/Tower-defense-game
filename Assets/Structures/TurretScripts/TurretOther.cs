using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretOther : MonoBehaviour
{

    private Transform childCircle;
    private float attackRadius;
    private bool showAttackRadius = false;
    public bool canClick = false;

    private void Awake()
    {
        childCircle = gameObject.transform.Find("shadow");
        childCircle.localScale = new Vector2(attackRadius * 4, attackRadius * 4);
    }


    private void FixedUpdate()
    {
        if (showAttackRadius) childCircle.gameObject.SetActive(true);
        else childCircle.gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (!canClick) return;

        if (Input.GetMouseButtonDown(0)) showAttackRadius = !showAttackRadius;

        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
