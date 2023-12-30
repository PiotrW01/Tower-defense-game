using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHandler : MonoBehaviour
{
    public Env objectType;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Delete) && !MapEditor.isMenu) 
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        MapEditor.envObjects.Remove(this.gameObject);
    }
}
