using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Lives))]
public class LivesEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("Lives are not manditory. This game object can be deleted or disabled without causing problems with the platformer game.", MessageType.Warning);
		EditorGUILayout.HelpBox("Additional lives pickup amounts and methods for respawn / restart should be added to this script for greater gameplay customisation.", MessageType.Info);
		EditorGUILayout.HelpBox("To lose a life call 'LoseLife' using SendMessage from an enemy, for example.", MessageType.Info);
		EditorGUILayout.HelpBox("To add life(s) call 'AddLifeOnPickup' using SendMessage from a pickup, for example.", MessageType.Info);
	}
}
