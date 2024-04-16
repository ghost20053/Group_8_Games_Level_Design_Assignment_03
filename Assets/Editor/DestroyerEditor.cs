using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Destroyer))]


public class DestroyerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("'DestroyGameObject' or 'DestroyChildGameObject' methods should be called from another script (e.g. Pickup, TriggerEnter) using SendMessage.", MessageType.Warning);

		EditorGUILayout.HelpBox("The Destroyer script will destroy the game object this script is attached to or a child game object with the specified delay.", MessageType.Info);
	}
}
