using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacing : MonoBehaviour
{
    public static GameObject heldObject;
    public static Env envObjectType;

    // Update is called once per frame
    void Update()
    {
        if (heldObject == null) return;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        heldObject.transform.position = newPos;

        if(Input.GetMouseButtonDown(0) && !MapEditor.isMenu)
        {
            GameObject newObj = Instantiate(heldObject, newPos, Quaternion.identity, GameObject.FindGameObjectWithTag("mapEnv").transform);
            newObj.AddComponent<ObjectHandler>();
            try
            {
                newObj.GetComponent<BoxCollider2D>().enabled = true;
            }
            catch
            {
                newObj.GetComponent<CircleCollider2D>().enabled = true;
            }
            newObj.GetComponent<ObjectHandler>().objectType = envObjectType;
            MapEditor.envObjects.Add(newObj);
        }
    }
}
