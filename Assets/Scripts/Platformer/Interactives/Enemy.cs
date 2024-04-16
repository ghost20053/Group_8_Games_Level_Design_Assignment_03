using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;				// The speed the enemy moves at.
	public bool canDie = false;					// Can the enemy be killed?
	public int scoreForKill = 100;				// How many points is killing an enemy worth?
	public int hitPoints = 1;					// How many times can the enemy be hurt before it dies?
	public Sprite deadEnemy;					// A sprite of the enemy when it's dead.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public bool flipOnStart = false;			// Change movement direction on start.
	public bool instantKill = false;			// Does the enemy kill on collision?
	public float repeatDamagePeriod = 2f; 		// How frequently we can hurt the player..
	public float hurtForceX = 10f; 				// The force with which the player is pushed when hurt.
	public float hurtForceY = 30f; 				// The force with which the player is pushed when hurt.

	public enum Behaviour { Hold, FreeRange, Patrol };
	public Behaviour behaviour = Behaviour.Hold;

	// if behaviour is patrol
	public enum PatrolType { PingPong, Cycle };
	public PatrolType patrolType = PatrolType.PingPong;
	public float arriveDistance = 0.3f;	// how close to consider I made it?
	public Transform[] patrolPoints;


	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private Transform topCheck;			// Reference to the position of the gameobject used for checking if the player is above.
	private float lastHitTime; 			// The time at which the player was last hit.
	private bool dead;					// Whether or not the enemy is dead.
	private SpriteRenderer ren;			// Reference to the sprite renderer.

	
	void Awake ()
	{
		// Setting up the references.
		ren = GetComponent<SpriteRenderer>();
		frontCheck = transform.Find("FrontCheck").transform;
		topCheck = transform.Find("TopCheck").transform;
	}


	void Start ()
	{
		// If flip on start is true...
		if (flipOnStart)
		{
			// Flip the enemy movement direction.
			Flip();
		}
	}


	// simple fixed update that has us move back and forwards between items marked as "obstacles"
	void FixedUpdate ()
	{

		// and peform the behaviour as asked for by the player
		switch (behaviour) 
		{
		case Behaviour.Hold:		BehaviourHoldUpdate(); 			break;
		case Behaviour.FreeRange:	BehaviourFreeRangeUpdate ();	break;
		case Behaviour.Patrol:		BehaviourPatrolUpdate();		break;
		}

	}


	void OnCollisionEnter2D (Collision2D other)
	{
		// If the colliding gameobject is an Enemy, look at if we should take damage...  (NB - only once per "contact" - then enemy & player are separated)
		if (other.gameObject.tag == "Player")
		{
			if ((other.transform.position.y > topCheck.position.y) && canDie)
			{
				// player is hitting from above...
				hitPoints--;

				// bounce the player
				other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().EnemyJump();

				// If the enemy has zero or fewer hit points and isn't dead yet...
				if (hitPoints <= 0 && !dead)
				{
					// ... call the death function.
					Death ();
					return;
				}

			}
			else
			{
				if (instantKill)
				{
					GameObject h = GameObject.Find("HealthText");
					if (h != null) h.SendMessage("LoseAllHealth");

					other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().Death(true);

					Vector2 hurtVector = new Vector2 ((other.gameObject.transform.position.x - transform.position.x) * hurtForceX, Vector2.up.y * hurtForceY);

					Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

					rb.velocity = new Vector2(0f, 0f);

					rb.AddForce(hurtVector);
				}
				else
				{
					// ... and if the time exceeds the time of the last hit plus the time between hits...
					if (Time.time > lastHitTime + repeatDamagePeriod) 
					{
						// ... apply damage and reset the lastHitTime.
						GameObject h = GameObject.Find("HealthText");
						if (h != null) h.SendMessage("SubtractHealthOnHit");

						other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().Hurt();

						Vector2 hurtVector = new Vector2 ((other.gameObject.transform.position.x - transform.position.x) * hurtForceX, Vector2.up.y * hurtForceY);

						Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

						rb.velocity = new Vector2(0f, 0f);

						rb.AddForce(hurtVector);

						lastHitTime = Time.time;
					}
				}
			}
		}
	}


	private void BehaviourHoldUpdate()
	{
		// chill, man
	}


	private void BehaviourFreeRangeUpdate()
	{
		// Create an array of all the colliders in front of the enemy.
		Collider2D[] frontHits = Physics2D.OverlapPointAll (frontCheck.position, 1);
		
		// Check each of the colliders.
		foreach (Collider2D c in frontHits) {
			// If any of the colliders is an Obstacle...
			if (c.tag == "Obstacle") {
				// ... Flip the enemy and stop checking the other colliders.
				Flip ();
				break;
			}
		}

		// Set the enemy's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);	
	}


	// if patrolling, be heading towards the next patrol point
	private Transform patrolTarget;
	private int patrolIndex = 0;
	private int patrolDI = 1;


	private void BehaviourPatrolUpdate()
	{
		// first use -- set up our first patrol target point
		if (patrolTarget == null) PatrolSetup ();

		// have we arrived near our patrol target
		if (IsPatrolWaypointArrived ()) PatrolWaypointAdvance ();


		// ensure we are heading in the right direction
		if (patrolTarget.position.x < transform.position.x && transform.localScale.x > 0)
			Flip ();
		if (patrolTarget.position.x > transform.position.x && transform.localScale.x < 0)
			Flip ();

		// Set the enemy's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (dead)
		{
			// Allow the enemy to rotate and spin it by adding a torque.
			GetComponent<Rigidbody2D>().freezeRotation = false;
			GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));
		}
	}


	private void PatrolSetup()
	{
		patrolIndex = 0;
		patrolTarget = patrolPoints[patrolIndex];
	}


	// have we arrived near our patrol target
	private bool IsPatrolWaypointArrived()
	{
		return (Mathf.Abs (patrolTarget.position.x - transform.position.x) < arriveDistance);
	}


	private void PatrolWaypointAdvance ()
	{
		// go to the next patrol point
		patrolIndex += patrolDI;

		// did we go off the end?
		if (patrolIndex >= patrolPoints.Length || patrolIndex < 0) 
		{
			// so do we go back to the beginning & keep counting up, or go back the other way through the points (ping pong)?
			if (patrolType == PatrolType.Cycle)
			{
				patrolIndex = 0;
			}

			else if (patrolType == PatrolType.PingPong)
			{
				patrolIndex -= patrolDI;	// undo the last add
				patrolIndex -= patrolDI;	// second time gets us to one before
				patrolDI = -patrolDI;		// and going back through the points the opposite direction
			}
		}

		// and this is the new target point
		patrolTarget = patrolPoints[patrolIndex];
	}


	void Death()
	{
		// set sprite to the deadEnemy sprite.
		ren.sprite = deadEnemy;

		// Increase the score by scoreForKill points
		GameObject s = GameObject.Find("ScoreText");
		if (s !=null) s.SendMessage("AddScoreForEnemyKill", scoreForKill);

		// Set dead to true.
		dead = true;

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play an audioclip....

	}


	private void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}

