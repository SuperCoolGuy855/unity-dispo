using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 sVal;
	public int hazardCount;
	public float sWait;
	public float wWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text waveCountText;
	private bool gameOver;
	private bool restart;
	private int score;
	private int waveCount;

	void Start()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		waveCountText.text = "";
		waveCount = 1;
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart && Input.GetKeyDown (KeyCode.R)) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	IEnumerator SpawnWaves()
	{
		waveCountText.text = "Wave " + waveCount;
		yield return new WaitForSeconds (sWait);
		waveCountText.text = "";
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) {
				Vector3 sPos = new Vector3 (Random.Range (-sVal.x, sVal.x), sVal.y, sVal.z);
				Quaternion sRot = Quaternion.identity;
				Instantiate (hazard, sPos, sRot);
				yield return new WaitForSeconds (sWait);
			}

			hazardCount = hazardCount * 2;
			waveCount++;
			waveCountText.text = "Wave " + waveCount;
			yield return new WaitForSeconds (wWait);
			waveCountText.text = "";

			if (gameOver) 
			{
				restartText.text = "Press R for restart";
				restart = true;
				break;
			}
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
