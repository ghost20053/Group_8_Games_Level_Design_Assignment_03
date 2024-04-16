using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeTimer : MonoBehaviour {
	private float curTime;
	private float bestTime = 10000f;
	private bool timerOn = false;
	private GameObject player;      // Reference to the player.
	private Text timerText;

	public Text bestTimeText;

	void Awake (){
		timerText = GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (timerOn) {
			curTime += Time.deltaTime;
		}
		timerText.text = "Time: " + (Mathf.Round (curTime * 100) / 100).ToString ();
	}

	public void TimerStart (){
		timerOn = true;
		curTime = 0;
	}

	public void TimerStop (){
		timerOn = false;
		UpdateBestTime();
	}

	private void UpdateBestTime()
	{
		if (curTime < bestTime)
		{
			bestTime = (Mathf.Round(curTime * 1000) / 1000);
			bestTimeText.text = "Best time: " +  bestTime.ToString();
		}
	}

	private void TimerReset (){
		curTime = 0;
		player.SendMessage("Respawn");
	}
}
