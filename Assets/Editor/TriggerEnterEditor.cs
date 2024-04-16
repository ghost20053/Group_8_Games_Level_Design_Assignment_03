using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TriggerEnter))]
public class TriggerEnterEditor : Editor 
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("You should have 2D physics matrix (Edit/Project Settings/Physics2D) set up to configure collisions with this trigger", MessageType.Warning);
		EditorGUILayout.HelpBox("Filter Tag can be used rather than the 2D physics matrix to filter more complex collision mechanics with this trigger.", MessageType.Info);
		EditorGUILayout.HelpBox("One or more messages can be set to be sent on collision enter with the trigger.", MessageType.Info);
	}
}
