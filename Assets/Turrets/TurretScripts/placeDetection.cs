using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class placeDetection : MonoBehaviour
{
    public bool canPlace = true;
    private bool isCollider = false;
    private SpriteRenderer sr;
    private UnityEngine.Color canPlaceColor = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 0.5f);
    private UnityEngine.Color canNotPlaceColor = new UnityEngine.Color(0.7f, 0, 0, 0.5f);

    private void Start()
    {
        sr = gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isCollider = true;
        canPlace = false;
        sr.color = canNotPlaceColor;
    }

    private void FixedUpdate()
    {
        if (isCollider)
        {
            isCollider = false;
            return;
        }
        else
        {
            canPlace = true;
            sr.color = canPlaceColor;
        }
    }


}
