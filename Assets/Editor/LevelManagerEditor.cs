using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUILayout.HelpBox("A Level Manger must be present in the scene.", MessageType.Warning);
		EditorGUILayout.HelpBox("Level Manger must be able to find the '2DCharacter' in the scene to enable/disable player input.", MessageType.Warning);
		EditorGUILayout.HelpBox("Level Manger must be able to find 'LevelImage' and 'LevelText' in the scene to show/hide the level text and background image.", MessageType.Warning);
		EditorGUILayout.HelpBox("The y padding value is used to have the player drop down from the checkpoint position, avoiding situations with the player respawning through the level geometry.", MessageType.Info);
		EditorGUILayout.HelpBox("Call 'ExitLevel' to end the level and transition to the next scene.", MessageType.Info);
	}
}
