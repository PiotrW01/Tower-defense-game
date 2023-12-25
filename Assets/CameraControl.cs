using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float maxSize = 5.4f;
    private float minSize = 1.0f;
    private float cameraBoundsHorizontal = 10.0f;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Do nothing if over UI
        }
        HandleMouseMovement();
        HandleKeyBoardMovement();
        ClampCamera();
    }


    public void ZoomIn()
    {
        Camera.main.orthographicSize /= 1.1f;
    }
    public void ZoomOut()
    {
        Camera.main.orthographicSize *= 1.1f;
    }
    public void ScrollDrag()
    {
        Vector3 move = Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
        move.z = -10;
        Camera.main.transform.position = move;
    }

    public void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(2)) mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.mouseScrollDelta.y > 0) ZoomIn();
        else if (Input.mouseScrollDelta.y < 0) ZoomOut();
        if (Input.GetMouseButton(2)) ScrollDrag();
    }

    public void HandleKeyBoardMovement()
    {
        Vector3 move = new(0,0,0);
        if (Input.GetKey(KeyCode.W)) move.y++;
        if (Input.GetKey(KeyCode.A)) move.x--;
        if (Input.GetKey(KeyCode.S)) move.y--;
        if (Input.GetKey(KeyCode.D)) move.x++;
        Camera.main.transform.position += move * cameraSpeed * Time.deltaTime;
    }
    public void ClampCamera()
    {
        Vector3 ClampedPos = Camera.main.transform.position;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
        ClampedPos.x = Mathf.Clamp(ClampedPos.x, -cameraBoundsHorizontal, cameraBoundsHorizontal);
        ClampedPos.y = Mathf.Clamp(ClampedPos.y, -cameraBoundsVertical, cameraBoundsVertical);
        Camera.main.transform.position = ClampedPos;
    }
}
