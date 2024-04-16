using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour 
{

	// inspector configuration
	[TooltipAttribute("Number of lives the player has before game over.")]
	[SerializeField] private int totalLives = 3;	// Number of lives the player has before game over.
	[TooltipAttribute("The number of lives a health pickup is worth.")]
	[SerializeField] private int livesPickup = 1;	// How many lives do I get for a health pickup?

	// the player's current score
	private int lives = 0;			// Keep track of the current lives the player has.

	private Text livesText;			// Reference to the lives text.

	private GameObject player;		// Reference to the player.

	void Awake ()
	{
		livesText = GetComponent<Text>();

		lives = totalLives;

		livesText.text = "Lives: " + lives;

		player = GameObject.FindGameObjectWithTag("Player");
	}


	public void LoseLife ()
	{
		lives--;

		if (lives < 0)
		{
			Restart();
			return;
		}

		livesText.text = "Lives: " + lives;

		Invoke("Respawn", 1.5f);
	}


	public void AddLifeOnPickup()
	{
		lives += livesPickup;

		livesText.text = "Lives: " + lives;
	}


	private void Respawn ()
	{
		player.SendMessage("Respawn");

		GameObject h = GameObject.Find("HealthText");
		if (h !=null) h.SendMessage("ResetHealth");
	}


	private void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
