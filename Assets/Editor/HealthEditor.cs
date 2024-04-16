using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Health))]
public class HealthEditor : Editor 
{
		public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("Health is not manditory. This game object can be deleted or disabled without causing problems with the platformer game.", MessageType.Warning);
		EditorGUILayout.HelpBox("Additional health amounts and methods should be added to this script for greater customisation.", MessageType.Info);
		EditorGUILayout.HelpBox("For instant death call 'LoseAllHealth' using SendMessage from a trigger, for example.", MessageType.Info);
		EditorGUILayout.HelpBox("For increments of damage call 'SubtractHealthOnHit' using SendMessage from a trigger or enemy, for example.", MessageType.Info);
	}
}
