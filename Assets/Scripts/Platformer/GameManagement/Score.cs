using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[TooltipAttribute("Score prefix. Default is 'Score: '")]
	[SerializeField] private string scorePrefix = "Score: "; 	// The score prefix.
	[TooltipAttribute("How many points is the pickup worth?")]
	[SerializeField] private int scoreForPickup = 10;			// How many points does this type of pickup get me.

	
	private int score = 0;		// The player's current score

	private Text scoreText;		// Reference to the Text component.

	void Awake ()
	{
		// Get the reference to the Text component.
		scoreText = GetComponent<Text>();
		// Set the score text.
		scoreText.text = scorePrefix + score;
	}


	// collected a certain kind of pickup -- add score
	public void AddScoreOnPickup()
	{
		score += scoreForPickup;
		scoreText.text = scorePrefix + score;
	}


	// killed a certain kind of enemy -- add score
	public void AddScoreForEnemyKill(int enemyScore)
	{
		score += enemyScore;
		scoreText.text = scorePrefix + score;
	}
}
