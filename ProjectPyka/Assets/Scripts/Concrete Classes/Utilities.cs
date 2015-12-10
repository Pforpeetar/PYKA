using UnityEngine;
using System.Collections;

public static class Utilities {
	public static float currentHealth = 100;
	public static int score;
	public static int enemyCount;
	public static int difficulty = 5;
	public static int maxEnemyDifficulty = 1;
	public static int minEnemyDifficulty = 1;
	public static int currLevel = 1;
	public static int maxSpawn = 25;
	public static int minSpawn = 1;

	public static Transform getPlayerTransform() {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			return GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		}
		return null;
	}
}

public enum EntityType{
	Player,Enemy
}
