using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Checkpoint))]

public class CheckpointEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("You should have 2D physics matrix (Edit/Project Settings/Physics2D) set up so that player is the only valid 'other' (enemies & background etc should not collide with this).", MessageType.Warning);
		EditorGUILayout.HelpBox("To play a SFX audio clip a checkpoint requires an Audio Source component. Use Add Component to attach an Audio Source component to this Game Object.", MessageType.Warning);
		EditorGUILayout.HelpBox("A Level Manager GameObject should be in the scene for the checkpoint functionality.", MessageType.Info);
	}
}
