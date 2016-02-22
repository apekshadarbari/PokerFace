using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PotManager))]
public class PotManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var pot = target as PotManager;
        GUI.enabled = false;
        EditorGUILayout.FloatField(new GUIContent("Total pot value"), pot.TotalPotValue);
    }
}