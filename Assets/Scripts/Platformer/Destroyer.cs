using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
	[TooltipAttribute("Whether or not this gameobject should be destroyed after a delay on Awake.")]
	[SerializeField] private bool destroyOnAwake =false ;			// Whether or not this game object should be destroyed after a delay on Awake.
	[TooltipAttribute("The delay in seconds before destroying the game object.")]
	[SerializeField] private float destroyDelay = 0.0f;		// The delay in seconds for destroying this game object on Awake.
	[TooltipAttribute("Whether or not to find a child game object and destroy it.")]
	[SerializeField] private bool findChild = false;		// Whether or not to find a child game object and destroy it.
	[TooltipAttribute("Name of the child game object to destroy with delay.")]
	[SerializeField] private string childName = null;				// Name of the child game object.


	void Awake()
	{
		// If the gameobject should be destroyed on awake...
		if(destroyOnAwake)
		{
			// ...are we looking for a child game object...
			if(findChild)
			{
				// ...destroy the child game object after a delay.
				Destroy (transform.Find(childName).gameObject, destroyDelay);
			}
			else
			{
				// ...destroy the gameobject after a delay.
				Destroy(gameObject, destroyDelay);
			}
		}
	}


	public void DestroyChildGameObject()
	{
		// Destroy this child gameobject, this can be called from an Animation Event.
		if(transform.Find(childName).gameObject != null)
			Destroy(transform.Find(childName).gameObject, destroyDelay);
	}


	public void DestroyGameObject()
	{
		// Destroy this game object after a delay, this can be called from an Animation Event.
		Destroy(gameObject, destroyDelay);
	}
}
