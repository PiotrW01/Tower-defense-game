using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseMapButtons : MonoBehaviour
{
    public TextMeshProUGUI mapText1;
    public TextMeshProUGUI mapText2;
    public TextMeshProUGUI mapText3;


    public void Clear()
    {
        mapText1.text = "";
        mapText2.text = "";
        mapText3.text = "";
    }

}
