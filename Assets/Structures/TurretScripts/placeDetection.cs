using UnityEngine;

public class placeDetection : MonoBehaviour
{
    public bool canPlace = true;
    private bool isCollider = false;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("aa" + collision.gameObject.layer);
        
        isCollider = true;
        canPlace = false;
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
        }
    }


}
