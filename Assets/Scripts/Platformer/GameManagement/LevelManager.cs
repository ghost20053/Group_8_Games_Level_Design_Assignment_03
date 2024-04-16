using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	//public GameObject	player;
	[TooltipAttribute("Amount of additional y height above the spawn point (checkpoint) y position. Default is 1.5 units.")]
	[SerializeField] private float yPadding = 1.5f;					// Amount of additional y height above the spawn point (checkpoint) y position.
	[TooltipAttribute("Level name displayed on load.")]
	[SerializeField] private string levelName = "World 1-1";		// Level name displayed on load.
	[TooltipAttribute("Time (in seconds) to wait before starting level. Default is 1.5 seconds.")]
	[SerializeField] private float levelStartDelay = 1.5f;			// Time (in seconds) to wait before starting level.
	[TooltipAttribute("Time (in seconds) to wait before ending level. Default is 0.5 seconds.")]
	[SerializeField] private float levelEndDelay = 0.5f;			// Time (in seconds) to wait before ending level.
	[TooltipAttribute("Index of the next level to load (from Build Settings). Default is 0, load Main Menu.")]
	[SerializeField] private int nextLevelIndex = 0;				// Index of the next level to load (from Build Settings).

	private Vector3	spawnPoint;			// The Vector3 value of the spawn point.
	private Text levelText;				// Reference to text to display on level load.
	private GameObject levelImage;		// Reference to the level background image to display on level load.
	private GameObject playerImage;		// Reference to the player background image to display during gameplay.
	private UnityStandardAssets._2D.Platformer2DUserControl playerControl;		// Reference to Platformer2DUserControl


	void Awake()
	{
		// Get a reference to the Platformer2DUserControl script
		playerControl = GameObject.Find("2DCharacter").GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();

		// Disable player control during level start.
		playerControl.DisableControl(true);

		// Get a reference to our image LevelImage by finding it by name.
		levelImage = GameObject.Find("LevelImage");

		// Set levelImage to acti blocking player's view of the game level.
		levelImage.GetComponent<Image>().enabled = true;

		// Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
		levelText = GameObject.Find("LevelText").GetComponent<Text>();

		// Set the text of levelText to the string "Day" and append the current level number.
		levelText.text = levelName;
			
		// Set levelImage to active blocking player's view of the game level.
		levelImage.SetActive(true);

		// Get a reference to our PlayerImage by finding it by name.
		playerImage = GameObject.Find("PlayerImage");

		// Set playerImage to inactive hiding it from player's view during level start.
		playerImage.SetActive(false);
		
		// Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Invoke("HideLevelImage", levelStartDelay);
	}


	void Start() 
	{
		// Find the player in the scene.
		Transform t = GameObject.FindGameObjectWithTag("Player").transform;
		// If we have a player, set our starting spawnpoint to the player's position.
		if (t !=null) spawnPoint = t.position;
	}


	//Hides level image at the start of a level.
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);

		// Enable playerImage gameObject.
		playerImage.SetActive(true);		

		// Enable player control by settig to true allowing player to move again.
		playerControl.DisableControl(false);
	}


	// Called by the Checkpoint's to set a new spawn point.
	public void SetSpawnPoint(Transform t)
	{
		// set our spawn point to be the position of the checkpoint with additional y padding above the checkpoint.
		spawnPoint = new Vector3(t.position.x, t.position.y + yPadding, t.position.z);

	}

	// Called to get the spawn point position when respawning the player.
	public Vector3 GetSpawnPoint()
	{
		return spawnPoint;
	}


	// Called to transition to the next level
	public void ExitLevel()
	{
		// Disable player control during level end.
		playerControl.DisableControl(true);

		// Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Invoke("LoadNextLevel", levelEndDelay);
	}


	private void LoadNextLevel()
	{
		SceneManager.LoadScene(nextLevelIndex);
	}
}
