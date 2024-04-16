using UnityEngine;
using System.Collections;

public class TriggerExit : MonoBehaviour 
{
	[TooltipAttribute("The method name to call with SendMessage. The message is sent to all script components attached to the collision exiting Game Object (e.g. the player).")]
	[SerializeField] private string messageOther = null;				// The method name to call with SendMessage. The message is sent to all script components attached to the collision exiting Game Object (e.g. the player).
	[TooltipAttribute("The method name to call with SendMessage. The message is sent to all script components attached to me (the trigger) on exit.")]
	[SerializeField] private string messageSelf = null;				// The method name to call with SendMessage. The message is sent to all script components attached to me (the trigger) on exit.
    [TooltipAttribute("The method name to call with BroadcastMessage. The message is sent to all script components and children attached to me (the trigger) on exit.")]
	[SerializeField] private string messageBroadcast = null;			// The method name to call with BroadcastMessage. The message is sent to all script components and children attached to me (the trigger) on exit.
	[TooltipAttribute("The Game Object to call SendMessage on. The message is sent to all script components attached to target on exit.")]
	[SerializeField] private GameObject messageTarget = null;			// The Game Object to call SendMessage on. The message is sent to all script components attached to target on exit.
	[TooltipAttribute("The method name to call with SendMessage. The message is sent to all script componenets attached to target (messageTarget) on exit.")]
	[SerializeField] private string messageTargetMessage = null;		// The method name to call with SendMessage. The message is sent to all script componenets attached to target (messageTarget) on exit.
	[TooltipAttribute("The tag to filter collisions.")]
	[SerializeField] private string filterTag = null;					// The tag to filter collisions.


	// action on collision exit
	void OnTriggerExit2D(Collider2D other)
	{
		// you could check other.tag == "Player" to be sure, however it should not be necessary
		// you should have 2D physics matrix (Edit/Project Settings/Physics2D) set up so that player is the only valid "other"

		// Do we have a filter tag...
		if (filterTag != "")
		{
			//... and did we just exit collide with an object with the filter tag...
			if (other.tag == filterTag)
			{
				//print ("Exited " + name);

				// send messages as requested
				ProcessMessage(other);
			}
		}
		else
		{
			// No filter tag? Send Message(s)
			ProcessMessage(other);
		}
	}


	private void ProcessMessage(Collider2D o)
	{
		// send messages as requested:
		if (messageOther != "") o.SendMessage(messageOther);		    // send a message to all scripts attached to other (the player / thing doing the exiting),
		if (messageSelf != "") SendMessage(messageSelf);				// send a message to all scripts attached to me (the trigger),
		if (messageBroadcast != "") BroadcastMessage(messageBroadcast); // send a message to all scripts attached to me and any children,
		if (messageTarget != null && messageTargetMessage != "") messageTarget.SendMessage(messageTargetMessage); // send a message to a particular game object.
	}
}
