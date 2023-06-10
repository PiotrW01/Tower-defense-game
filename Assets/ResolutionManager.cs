using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int defaultWidth = 1280;
    public int defaultHeight = 720;
    private bool fullScreen = false;

    private void Start()
    {
        Screen.SetResolution(defaultWidth, defaultHeight, false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
        {
            ToggleFullscreen();
        }
    }

    private void ToggleFullscreen()
    {
        fullScreen = !fullScreen;

        if (fullScreen)
        {
            Screen.SetResolution(1920, 1080, true);
        } else
        {
            Screen.SetResolution(defaultWidth, defaultHeight, false);
        }
    }
}
