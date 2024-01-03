using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCameraControl : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float maxSize = 60f;
    private float minSize = 20f;
    private float cameraBoundsHorizontal = 100.0f;
    private float cameraBoundsVertical;
    private float cameraSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        cameraBoundsVertical = cameraBoundsHorizontal * 9 / 16;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseMovement();
        HandleKeyBoardMovement();
        ClampCamera();
    }


    public void ZoomIn()
    {
        Camera.main.fieldOfView /= 1.1f;
    }
    public void ZoomOut()
    {
        Camera.main.fieldOfView *= 1.1f;
    }

    public void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(2)) mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.mouseScrollDelta.y > 0) ZoomIn();
        else if (Input.mouseScrollDelta.y < 0) ZoomOut();
    }

    public void HandleKeyBoardMovement()
    {
        Vector3 move = new(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) move.z++;
        if (Input.GetKey(KeyCode.A)) move.x--;
        if (Input.GetKey(KeyCode.S)) move.z--;
        if (Input.GetKey(KeyCode.D)) move.x++;
        Camera.main.transform.position += move * cameraSpeed * Time.deltaTime;
    }
    public void ClampCamera()
    {
        Vector3 ClampedPos = Camera.main.transform.position;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minSize, maxSize);
        ClampedPos.x = Mathf.Clamp(ClampedPos.x, -cameraBoundsHorizontal, cameraBoundsHorizontal);
        ClampedPos.z = Mathf.Clamp(ClampedPos.z, -cameraBoundsVertical, cameraBoundsVertical);
        Camera.main.transform.position = ClampedPos;
    }
}
