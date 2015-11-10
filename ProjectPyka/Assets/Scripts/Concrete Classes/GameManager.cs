using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public int difficulty;
	public int tempSpawnedDifficulty;
	public int maxEnemyDifficulty;
	public int minEnemyDifficulty;
	public int tempLevelCounter;
	public int maxSpawn;
	public int minSpawn;
	public int score;
	public List<GameObject> EnemyList = new List<GameObject>();
	public GameObject tempEnemy;

	// Use this for initialization
	void Start () {
		minEnemyDifficulty = 1;
		maxEnemyDifficulty = 10;
		minSpawn = 1;
		maxSpawn = 100;
		LevelBegin (difficulty);
	}

	void LevelBegin(int currLevel) {
		tempSpawnedDifficulty = 0;
		int tempDiff;
		while (tempSpawnedDifficulty <= difficulty) {
			//tempDiff = Random.Range (minEnemyDifficulty, maxEnemyDifficulty);
			//create enemy of that diff
			Instantiate(tempEnemy);
			tempSpawnedDifficulty++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
