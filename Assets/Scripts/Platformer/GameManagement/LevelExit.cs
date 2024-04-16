using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour 
{

	[SerializeField] private float	waitTime = 0.5f;
	[SerializeField] private string levelName = null;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Invoke("LoadNextLevel", waitTime);
		}
	}


	private void LoadNextLevel ()
	{
		SceneManager.LoadScene(levelName);
	}
}
