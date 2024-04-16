using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Toggle))]

public class ToggleEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("'ToggleActive' method should be called from another script (e.g. Pickup, TriggerEnter) using SendMessage. The toggle requires a reference to a Game Object in the scene to work. Without a target nothing will happen.", MessageType.Warning);

		EditorGUILayout.HelpBox("The toggle script will immediately set an active game object to inactive, and an inactive game object to active.", MessageType.Info);
	}
}
