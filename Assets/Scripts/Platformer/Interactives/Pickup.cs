using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour 
{
	[TooltipAttribute("Audio Clip to play on pickup.")]
	[SerializeField] private AudioClip sfx = null;					// Audio Clip to play on pickup.
	[TooltipAttribute("The message (method name) used with SendMessage. The message is sent to all script components attached to me (the pickup).")]
	[SerializeField] private string messageSelf = null;			// The method name to call with SendMessage. The message is sent to all script components attached to me (the pickup).
	[TooltipAttribute("The message (method name) used with SendMessage. The message is sent to all script components attached to the colliding GameObject (the player).")]
	[SerializeField] private string messageOther = null;			// The method name to call with SendMessage. The message is sent to all script components attached to the colliding Game Object (the player).
    [TooltipAttribute("The method name to call with BroadcastMessage. The message is sent to all script components and children attached to me (the pickup).")]
	[SerializeField] private string messageBroadcast = null;		// The method name to call with BroadcastMessage. The message is sent to all script components and children attached to me (the pickup).
	[TooltipAttribute("The GameObject to call SendMessage on. The message is sent to all script components attached to target.")]
	[SerializeField] private GameObject messageTarget = null; 		// The Game Object to call SendMessage on. The message is sent to all script components attached to target.
	[TooltipAttribute("The method name to call with SendMessage. The message is sent to all script componenets attached to target (messageTarget).")]
	[SerializeField] private string messageTargetMessage = null;	// The method name to call with SendMessage. The message is sent to all script componenets attached to target (messageTarget).

	private AudioSource sfxSource;				// Reference to pickup's Audio Source component.


	void Awake()
	{
		// get a reference to the pickup's audio source component.
		sfxSource = this.gameObject.GetComponent<AudioSource>();
		// if sound effects are specified and we have a audio source attached...
		if (sfx != null && sfxSource != null) sfxSource.clip = sfx;		// ...set the audio clip on audio source to our pickup sfx.
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// you should have 2D physics matrix (Edit/Project Settings/Physics2D) set up so that player is the only valid "other"
		// (enemies & background etc should not collide with this)

		// print ("Pickup " + name + " Collected");

		// play sound effects if specified.
		if (sfx != null && sfxSource != null) sfxSource.Play();

		// could instantiate a particle effect...

		// send messages as requested:
		if (messageOther != "") other.SendMessage(messageOther);		// send a message to all scripts attached to other (the player / thing doing the picking up),
		if (messageSelf != "") SendMessage(messageSelf);				// send a message to all scripts attached to me (the pickup),
		if (messageBroadcast != "") BroadcastMessage(messageBroadcast); // send a message to all scripts attached to me and any children,
		if (messageTarget != null && messageTargetMessage != "") messageTarget.SendMessage(messageTargetMessage); // send a message to a particular game object.

		// destroy me (I am the pickup) so that I can't be collected again.
		if (sfx != null && sfxSource != null)
		{
			Destroy(this);		// Destroy this script component.
			gameObject.GetComponent<SpriteRenderer>().enabled = false; 		// Disable the sprite.
			Destroy(gameObject, sfx.length);		// Destroy the pickup after the sfx audio length.
		}
		else
		{
			Destroy (gameObject);
		}
	}
}
