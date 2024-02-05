using UnityEngine;

public class placeDetection : MonoBehaviour
{
    public bool canPlace = true;
    private bool isCollider = false;


    private void OnCollisionStay(Collision collision)
    {
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
