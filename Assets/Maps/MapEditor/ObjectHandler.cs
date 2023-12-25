using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHandler : MonoBehaviour
{
    public Env objectType;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Delete) && !EventSystem.current.IsPointerOverGameObject()) Destroy(this.gameObject);
    }
}
