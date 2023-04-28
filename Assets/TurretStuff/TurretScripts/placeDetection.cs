using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class placeDetection : MonoBehaviour
{
    [HideInInspector]
    public bool canPlace = true;
    private bool isTrigger = false;
    private bool isCollider = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("path")) return;
        canPlace = false;
        isTrigger = true;
        gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("path")) return;
        isTrigger = false;
        if (isCollider) return;
        canPlace = true;
        gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 0.5f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("tower")) return;
        canPlace = false;
        isCollider = true;
        gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.7f, 0, 0, 0.5f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("tower")) return;
        isCollider = false;
        if (isTrigger) return;
        canPlace = true;
        gameObject.transform.Find("shadow").GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 0.5f);
    }

}
