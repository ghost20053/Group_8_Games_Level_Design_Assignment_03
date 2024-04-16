using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAdditive : MonoBehaviour 
{
	[TooltipAttribute("The Vector 2 (x,y) force to apply using AddForce on trigger stay.")]
	[SerializeField] private Vector2 bounceForce = new Vector2(0f, 50f);	// The Vector 2 (x,y) force to apply using AddForce.
	[TooltipAttribute("The tag string to filter collisions.")]
	[SerializeField] private string filterTag = null;								// The tag string to filter colissions.


	void OnTriggerStay2D(Collider2D other)
	{
		// Do we have a filter tag...
		if (filterTag != "")
		{
			// ...and do we keep colliding with a game object with that tag...
			if (other.tag == filterTag)
			{
				// ...apply bounce.
				BounceRigidbody(other.GetComponent<Rigidbody2D>());
			}
		}
		else
		{
			// No filter, apply bounce.
			BounceRigidbody(other.GetComponent<Rigidbody2D>());
		}
	}


	private void BounceRigidbody(Rigidbody2D rb)
	{
		// Add the bounce force to the other rigidbody.
		rb.AddForce(bounceForce);
	}
}
