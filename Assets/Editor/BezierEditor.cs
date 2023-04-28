using Codice.Client.GameUI.Explorer;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(BezierCurveGenerator))]
public class BezierEditor : Editor
{
        BezierCurveGenerator myScript;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        myScript = (BezierCurveGenerator)target;

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Start Curve"))
        {
            myScript.isRunning = true;
            myScript.StartCoroutine(MyCoroutine());
        }

        if (GUILayout.Button("Stop Curve"))
        {
            myScript.isRunning = false;
        }

        if (GUILayout.Button("Delete Curve"))
        {
            myScript.ClearPath();
        }

        EditorGUILayout.EndHorizontal();
    }

    private IEnumerator MyCoroutine()
    {
        while(true)
        {
            if (!myScript.isRunning)
            {
                yield break;
            }
            myScript.ClearPath();
            myScript.Generate();
            yield return new WaitForSeconds(0.5f);
        }
    }

}
