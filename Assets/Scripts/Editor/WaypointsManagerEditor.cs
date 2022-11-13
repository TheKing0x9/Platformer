using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Platformer.Managers;
using Platformer.Utilities;
using System;

[CustomEditor(typeof(WaypointManager))]
public class WaypointManagerEditor : Editor
{
    WaypointManager creator;

    public override void OnInspectorGUI()
    {
        creator = (WaypointManager)target;

        if (!creator.IsEmpty())
        {
            SerializedProperty showPathway_Property = serializedObject.FindProperty("m_visualizePath");
            EditorGUILayout.PropertyField(showPathway_Property, new GUIContent("Display Pathway: "), GUILayout.Height(20));
        }

        if (GUILayout.Button("Add New Waypoint"))
            creator.CreateWaypoint();
        
        if (!creator.IsEmpty() && GUILayout.Button("Delete Last Waypoint"))
            creator.DeleteWaypoint();

        if (!creator.IsEmpty() && GUILayout.Button("Clear Waypoints"))
            creator.ClearWaypoints();     

        serializedObject.ApplyModifiedProperties();
    }

}
