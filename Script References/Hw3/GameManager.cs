using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Enemy[] enemies;
	private int hasEnemies;
	// Use this for initialization
	void Start () {
		hasEnemies = enemies.Length;
	}
	
	// Update is called once per frame
	void Update () {
		hasEnemies = 0;
		for (int i =0; i < enemies.Length; i++) {
			if (enemies[i] != null) {
				hasEnemies++;
			}
		}

		if (hasEnemies == 0) {
			//Time.timeScale = 0;
		}
	}

	void OnGUI() {
		if (hasEnemies == 0) {
			GUI.Label (new Rect (100, 100, 100, 100), "You win!");
			/*if (GUI.Button(new Rect (150, 100, 100, 100), "Reset?")) {
				Time.timeScale = 1;
				Application.LoadLevel(0);

			}*/
		}
	}

}
