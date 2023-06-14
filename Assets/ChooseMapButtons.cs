using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseMapButtons : MonoBehaviour
{
    public TextMeshProUGUI mapText1;
    public TextMeshProUGUI mapText2;
    public TextMeshProUGUI mapText3;

    private void Start()
    {
        Clear();
        mapText1.text = "Selected";
        MapManager.ChosenMap = 0;
    }

    public void ChooseMap1()
    {
        SoundManager.Instance.PlayButtonClick();
        Clear();
        mapText1.text = "Selected";
        MapManager.ChosenMap = 0;
    }
    public void ChooseMap2()
    {
        SoundManager.Instance.PlayButtonClick();
        Clear();
        mapText2.text = "Selected";
        MapManager.ChosenMap = 1;
    }
    public void ChooseMap3()
    {
        SoundManager.Instance.PlayButtonClick();
        Clear();
        mapText3.text = "Selected";
        MapManager.ChosenMap = 2;
    }



    public void Clear()
    {
        mapText1.text = "";
        mapText2.text = "";
        mapText3.text = "";
    }

}
