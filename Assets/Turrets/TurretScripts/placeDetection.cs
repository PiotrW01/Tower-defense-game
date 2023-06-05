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

/*    private void OnTriggerStay2D(Collider2D collision)
    {
        canPlace = false;
        isTrigger = true;
        sr.color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
    }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
        isCollider = true;
        canPlace = false;
        sr.color = canNotPlaceColor;
    }

    /*    private void OnTriggerEnter2D(Collider2D collision)
        {
            canPlace = false;
            isTrigger = true;
            sr.color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
            //gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
        }*/

    /*    private void OnTriggerExit2D(Collider2D collision)
        {
            isTrigger = false;
            if (isCollider) return;
            canPlace = true;
            sr.color = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 0.5f);
            //gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 0.5f);
        }*/


    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            canPlace = false;
            isCollider = true;
            sr.color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
        }*/

    /*    private void OnCollisionExit2D(Collision2D collision)
        {
                isCollider = false;
        }*/


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
