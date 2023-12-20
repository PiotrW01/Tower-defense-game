using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadTest : MonoBehaviour
{
    public void Load()
    {
        EditorUtility.OpenFilePanel("choose map","",".obj");
    }
}
