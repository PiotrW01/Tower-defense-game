using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator BuyMenuAnimator;
    private string Menu = "IsBuyMenuOpen";

    private void Start()
    {
        //BuyMenuAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BuyMenuAnimator.SetBool(Menu, !BuyMenuAnimator.GetBool(Menu));
        }
    }
}
