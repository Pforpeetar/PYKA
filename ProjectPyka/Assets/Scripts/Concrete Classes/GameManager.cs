using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public int tempSpawnedDifficulty;
	public bool isVictorious = false;
	public GameObject instantiatedPlayer;
	public GameObject playerPrefab;
	public GameObject[] enemyType;
	public GameObject startPos;
	//public List<GameObject> enemyTypeList = new List<GameObject>();
	public List<GameObject> enemySpawnLocations = new List<GameObject> ();
	private List<GameObject> levelEnemyList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instantiatedPlayer = GameObject.FindGameObjectWithTag ("Player");
		tempSpawnedDifficulty = 0;
		LevelBegin (Utilities.difficulty);

	}

	void LevelBegin(int currLevel) {
		tempSpawnedDifficulty = 0;
		Utilities.enemyCount = 0;
		int tempDiff;
		GameObject tempEnemy;

		while (tempSpawnedDifficulty < Utilities.difficulty) {
			//tempDiff = Random.Range (minEnemyDifficulty, maxEnemyDifficulty);
			//create enemy of that diff
			int i = Random.Range(0, enemyType.Length);
			int s = Random.Range(0, enemySpawnLocations.Count);
			int enemyLevel = enemyType[i].GetComponent<Enemy>().level;
			if (enemyLevel <= Utilities.maxEnemyDifficulty && ((tempSpawnedDifficulty + enemyLevel) <= Utilities.difficulty)) {
			tempSpawnedDifficulty += enemyLevel;
			tempEnemy = (GameObject) Instantiate (enemyType[i], enemySpawnLocations[s].transform.position, Quaternion.identity);
			levelEnemyList.Add (tempEnemy);
			Utilities.enemyCount++;
			tempSpawnedDifficulty++;
			}

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
			Utilities.currentHealth = instantiatedPlayer.GetComponent<Player>().health;
			Time.timeScale = 1;

		} else {
			// do Lose Stuff:
			//    1) Lose Screen
			//    2) Print LevelsSurvived
			//    3) Options 
			//       a) Main Menu
			//       b) Restart
			DestroyAllEnemies();
			Utilities.difficulty = 5;
			Utilities.currentHealth = playerPrefab.GetComponent<Player>().maxHealth;
			Utilities.currLevel = 1;
			Utilities.minEnemyDifficulty = 1;
			Utilities.maxEnemyDifficulty = 1;
			Utilities.minSpawn = 1;
			Utilities.maxSpawn = 100;
			Time.timeScale = 1;
			Application.LoadLevel(3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Utilities.maxEnemyDifficulty);
		//Debug.Log (instantiatedPlayer);
		if (instantiatedPlayer.Equals(null)) {
			isVictorious = false;
			LevelEnd (isVictorious);
		}
		if (Utilities.enemyCount <= 0) {
			isVictorious = true;
			LevelEnd (isVictorious);
		}
		if (instantiatedPlayer.Equals(null) 
		    && Input.GetKey(KeyCode.N)) {
			Time.timeScale = 1;
			Utilities.difficulty = 10;
			instantiatedPlayer = (GameObject) Instantiate (playerPrefab, 
			                     						   startPos.transform.position, 
			                                  			   Quaternion.identity);
			LevelBegin(Utilities.difficulty);
		}
	}

	//text for the score
	void OnGUI() {
		if (isVictorious) {
			if (GUI.Button (new Rect(Screen.width/2, Screen.height/2, Screen.width/4, Screen.height/20),"End Game")) {
				//print ("Clicked End Game");
				Application.Quit();
			}
			if (GUI.Button (new Rect(Screen.width/2, Screen.height/2.5f, Screen.width/4, Screen.height/20),"Next Level")) {
				//print ("Clicked End Game");
				Time.timeScale = 1;
				Utilities.difficulty += 5;
				Utilities.currLevel++;
				Utilities.maxEnemyDifficulty += 5;
				Application.LoadLevel(2);
			}
		}
	}
}
