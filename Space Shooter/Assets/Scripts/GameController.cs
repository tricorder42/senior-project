﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public float hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	private int score;
	public Text levelText;
	private int level;
	public Text restartText;
	public Text gameOverText;
	private bool gameOver = false;


	void Start () {
		StartCoroutine (SpawnWaves ());
		UpdateScore ();
		UpdateLevel ();
		restartText.text = "";
		gameOverText.text = "";
	}

	void SpawnHazard () {
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Instantiate (hazards[Mathf.FloorToInt(Random.Range (0, hazards.Length))], spawnPosition, Quaternion.identity);
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		LevelUp ();
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				SpawnHazard ();
				yield return new WaitForSeconds (spawnWait);
			}
			hazardCount = hazardCount * 1.25f;
			spawnWait = Mathf.Clamp (spawnWait * 0.8f, 0.2f, 0.5f);
			waveWait = Mathf.Clamp (waveWait * 0.9f, spawnWait * 20f, 10f);
			yield return new WaitForSeconds (waveWait);
			LevelUp ();
		}
	}

	public void AddScore(int deltaScore) {
		score += deltaScore;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void LevelUp() {
		if (!gameOver) AddScore (level);
		level++;
		UpdateLevel ();
	}
	
	void UpdateLevel () {
		levelText.text = "Level: " + level;
	}

	public void GameOver() {
		restartText.text = "Press 'R' for Restart";
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	void Update () {
		if (gameOver && Input.GetButtonDown ("Restart")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	/*
	public float nextFire;
	public float delayFactor;
	public float ramp;
	// Update is called once per frame
	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + delayFactor*(Mathf.Log (Time.time + ramp, ramp) / Time.time);
			SpawnWaves ();
		}
	
	}

	*/
}
