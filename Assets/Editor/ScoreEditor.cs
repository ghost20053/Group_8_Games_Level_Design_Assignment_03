using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Score))]
public class ScoreEditor : Editor 
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("Score is not manditory. This game object can be deleted or disabled without causing problems with the platformer game.", MessageType.Warning);
		EditorGUILayout.HelpBox("Additional score amounts and methods should be added to this script for greater customisation.", MessageType.Info);
		EditorGUILayout.HelpBox("To add to the score call 'AddScoreOnPickup' using SendMessage from a pickup, for example.", MessageType.Info);
	}
}
