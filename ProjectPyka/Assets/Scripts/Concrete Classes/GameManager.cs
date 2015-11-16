using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public int difficulty;
	public int tempSpawnedDifficulty;
	public int maxEnemyDifficulty;
	public int minEnemyDifficulty;
	public int currLevel;
	public int maxSpawn;
	public int minSpawn;
	public int score;
	public bool isVictorious = false;
	public GameObject instantiatedPlayer;
	public GameObject playerPrefab;
	public GameObject enemy;
	public GameObject startPos;
	//public List<GameObject> enemyTypeList = new List<GameObject>();
	public List<GameObject> enemySpawnLocations = new List<GameObject> ();
	public List<GameObject> levelEnemyList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instantiatedPlayer = GameObject.FindGameObjectWithTag ("Player");
		minEnemyDifficulty = 1;
		maxEnemyDifficulty = 10;
		minSpawn = 1;
		maxSpawn = 100;
		LevelBegin (difficulty);
	}

	void LevelBegin(int currLevel) {
		tempSpawnedDifficulty = 0;
		int tempDiff;
		GameObject tempEnemy;
		while (tempSpawnedDifficulty <= difficulty) {
			//tempDiff = Random.Range (minEnemyDifficulty, maxEnemyDifficulty);
			//create enemy of that diff
			tempEnemy = Instantiate (enemy);
			levelEnemyList.Add (tempEnemy);
			tempSpawnedDifficulty++;
		}
	}

	void DestroyAllEnemies() {
		foreach (GameObject enemiesLeft in levelEnemyList) {
			if (!enemiesLeft.Equals(null)) {
				GameObject.Destroy(enemiesLeft);
			}
		}
		levelEnemyList.Clear();
	}

	void LevelEnd(bool victoryCondition) {
		if (victoryCondition) {
			// do Victory Stuff:
			//    1) Victory Screen 
			//    2) Click to Load new level
			//    3) LevelsSurvived++
			DestroyAllEnemies();
			Time.timeScale = 0;
		} else {
			// do Lose Stuff:
			//    1) Lose Screen
			//    2) Print LevelsSurvived
			//    3) Options 
			//       a) Main Menu
			//       b) Restart
			DestroyAllEnemies();
			Time.timeScale = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (instantiatedPlayer);
		if (instantiatedPlayer.Equals(null)) {
			isVictorious = false;
			LevelEnd (isVictorious);
		}
		if (levelEnemyList.Count <= 0) {
			isVictorious = true;
			LevelEnd (isVictorious);
		}
		if (instantiatedPlayer.Equals(null) 
		    && Input.GetKey(KeyCode.N)) {
			Time.timeScale = 1;
			difficulty = 10;
			instantiatedPlayer = (GameObject) Instantiate (playerPrefab, 
			                     						   startPos.transform.position, 
			                                  			   Quaternion.identity);
			LevelBegin(difficulty);
		}
	}
}
