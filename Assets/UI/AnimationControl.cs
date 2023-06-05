using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator BuyMenuAnimator;
    private string Menu = "IsBuyMenuOpen";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenCloseMenu();
        }
    }

    public void OpenCloseMenu()
    {
        SoundManager.Instance.PlayButtonClick();
        BuyMenuAnimator.SetBool(Menu, !BuyMenuAnimator.GetBool(Menu));
    }

}
