using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Platformer.Utilities;

[CustomEditor(typeof(Waypoint)), CanEditMultipleObjects]
public class WaypointsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Waypoint self = (Waypoint)target;

        if (!self.waypointManager)
            return;

        if (!self.isLooped && GUILayout.Button("Add Next Waypoint"))
            Selection.activeGameObject = self.waypointManager.CreateWaypoint().gameObject;

        if (!self.isLooped && self.nextWaypoint == null && GUILayout.Button("Loop to Existing Waypoint"))
        {
            self.isLooped = true;
        }

        if (self.isLooped)
        {
            SerializedProperty loopedWaypoint_Property = serializedObject.FindProperty("nextWaypoint");
            EditorGUILayout.PropertyField(loopedWaypoint_Property, new GUIContent("Looping Waypoint"), GUILayout.Height(20));
        }

        if (self.isLooped && GUILayout.Button("Clear Loop to Existing Waypoint"))
        {
            self.isLooped = false;
            self.nextWaypoint = null;
        }

        if (self.nextWaypoint != null && GUILayout.Button("Insert New Waypoint"))//Cant Insert a waypoint if there if the current one isnot wedged between another
            Selection.activeGameObject = self.waypointManager.InsertWaypoint(self).gameObject;

        if (GUILayout.Button("Delete This Waypoint"))
            DestroyImmediate(self.gameObject);

        if (self)
            serializedObject.ApplyModifiedProperties();
    }

}
