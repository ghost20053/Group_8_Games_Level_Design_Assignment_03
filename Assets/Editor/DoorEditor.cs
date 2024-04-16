using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("'Open' method should be called from another script (e.g. Pickup, TriggerEnter) using SendMessage.", MessageType.Warning);

		EditorGUILayout.HelpBox("The Door script will immediately destroy the game object this script is attached to.", MessageType.Info);
	}
}
