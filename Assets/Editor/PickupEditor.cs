using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Pickup))]
public class PickupEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();		// display all other non-HideInInspector fields.

		EditorGUILayout.HelpBox("You should have 2D physics matrix (Edit/Project Settings/Physics2D) set up so that player is the only valid 'other' (enemies & background etc should not collide with this).", MessageType.Warning);
		EditorGUILayout.HelpBox("To play a SFX audio clip a pickup requires an Audio Source component. Use Add Component to attach an Audio Source component to this GameObject.", MessageType.Warning);
		EditorGUILayout.HelpBox("One or more messages can be set to be sent on collision with the pickup.", MessageType.Info);
	}
}
