using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	// When the Open method is called from a SendMessage from another script...
	public void Open()
	{
		// I could play an animation, a sound, etc...

		// Destroy the door game object.
		Destroy (gameObject);
	}
}
