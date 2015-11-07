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
	}

	void LevelBegin(int currLevel) {
		tempSpawnedDifficulty = 0;
		Random rnd = new Random();
		int tempDiff;
		while (tempSpawnedDifficulty <= difficulty) {
			tempDiff = rnd.Next (minEnemyDifficulty, maxEnemyDifficulty);
			//create enemy of that diff
			tempSpawnedDifficulty += tempDiff;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
