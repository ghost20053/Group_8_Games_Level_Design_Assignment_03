using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{

	// inspector configuration
	[SerializeField] private float totalHealth = 100f;	// how many points does this type of pickup get me?
	[SerializeField] private bool clampHealth = true;
	[SerializeField] private float pickupHealth = 25f;	// how many points does this type of pickup get me?
	[SerializeField] private float damage = 10f;
	[SerializeField] private float mineDamage = 20f;
	// the player's current health
	private float health = 0;
	private Text healthText;
	private GameObject player;

	//private GameObject player;

	void Awake ()
	{
		healthText = GetComponent<Text>();

		health = totalHealth;

		healthText.text = "Health: " + (int)health;

		player = GameObject.FindGameObjectWithTag("Player");
	}


	public void LoseAllHealth ()
	{
		health = 0;

		CheckHealth();

		healthText.text = "Health: " + (int)health;
	}


	public void AddHealthPickup ()
	{
		health += pickupHealth;

		if (clampHealth && health > totalHealth)
			health = totalHealth;

		healthText.text = "Health: " + (int)health;
	
	}


	public void SubtractHealthOverTime ()
	{
		health -= damage * Time.fixedDeltaTime;

		CheckHealth();

		if (health < 0)
			health = 0;

		healthText.text = "Health: " + (int)health;

	}


	public void SubtractHealthOnHit ()
	{
		health -= damage;

		CheckHealth();

		if (health < 0)
			health = 0;

		healthText.text = "Health: " + (int)health;
	}

	public void SubtractMineDamage()
	{
		health -= mineDamage;

		CheckHealth();

		if (health < 0)
			health = 0;

		healthText.text = "Health: " + (int)health;
	}

	public void ResetHealth ()
	{
		health = totalHealth;

		healthText.text = "Health: " + (int)health;
	}


	private void CheckHealth ()
	{
		
		if (health <= 0)
		{
			player.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().Death(true);
		}
		
	}


}
