using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	[TooltipAttribute("The audio clip to play on reaching the checkpoint.")]
	[SerializeField] private AudioClip sfx = null;		// The audio clip to play on reaching the checkpoint.

	private GameObject levelManager;			// Reference to the Level Manager.
	private AudioSource sfxSource;				// Reference to the checkpoint's Audio Source component.
	private GameObject	checkpointInactive;		// Reference to the inactive checkpoint sprite.
	private GameObject	checkpointActive;		// Reference to the active checkpoint sprite.


	void Awake()
	{
		// get a reference to the checkpoint's audio source component.
		sfxSource = this.gameObject.GetComponent<AudioSource>();
		// if sound effects are specified and we have a audio source attached...
		if (sfx != null && sfxSource != null) sfxSource.clip = sfx;		// ...set the audio clip on audio source to our checkpoint sfx.

		// Get reference to the CheckpointInactive sprite game object.
		checkpointInactive = transform.Find("CheckpointInactive").gameObject;

		// Set the checkpoint to inactive (game is starting).
		checkpointInactive.SetActive(true);

		// Get reference to the CheckpointActive sprite game object.
		checkpointActive = transform.Find("CheckpointActive").gameObject;

		// Checkpint is inactive, disable active sprite
		checkpointActive.SetActive(false);
	}


	void Start()
	{
		levelManager = GameObject.Find("LevelManager");		// Find the LevelManager Game Object in the scene and keep a reference.
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// you should have 2D physics matrix (Edit/Project Settings/Physics2D) set up so that player is the only valid "other"
		// (enemies & background etc should not collide with this)

		//print ("Checkpoint " + name + " Reached");

		// play sound effects if specified.
		if (sfx != null && sfxSource != null) sfxSource.Play();

		// could instantiate a particle effect...
		// could play an animation...

		// if the scene has a level manager...
		if (levelManager != null) levelManager.SendMessage("SetSpawnPoint", transform);		// ...set the spawn point to this (checkpoint) location.

		// Set the checkpoint to appear active.
		checkpointInactive.SetActive(false);
		checkpointActive.SetActive(true);

		// Destroy this script so that the checkpoint is not triggered again.
		Destroy(this);
	}
}
